using Markitos.Server.Models;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Markitos.Server.Data
{
    public class StoryDB : DbContext
    {
        public virtual DbSet<DBStoryModel> Stories { get; set; }

        public StoryDB(DbContextOptions<StoryDB> options) : base(options)
        {
            var con = (Microsoft.Data.SqlClient.SqlConnection)Database.GetDbConnection();
            if (con.ConnectionString.Contains("(localdb)", StringComparison.OrdinalIgnoreCase))
            {
                return; // no MSI needed when using local db
            }
            // force sync 
            con.AccessToken = new AzureServiceTokenProvider()
                .GetAccessTokenAsync("https://database.windows.net/")
                .Result;
        }
    }
}
