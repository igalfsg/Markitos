﻿using Markitos.Server.Data;
using Markitos.Server.Models;
using Markitos.Shared.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Markitos.Server.Manager
{
    public class StoryManagercs
    {
        private readonly StoryDB _storyDB;
        private readonly ILogger<StoryManagercs> _logger;
        public StoryManagercs(StoryDB storyDB, ILogger<StoryManagercs> logger)
        {
            _storyDB = storyDB;
            _logger = logger;
        }

        public async Task<string> AddStoryAsync(DBStoryModel story)
        {
            if(story == null)
            {
                return "error adding your story please try again later.";
            }
            try
            {
                _storyDB.Stories.Add(story);
                await _storyDB.SaveChangesAsync();
                return "Thanks for sharing :)";
            }
            catch(Exception ex)
            {
                _logger.LogError("error adding", ex);
                return "error adding your story please try again later.";
            }
        }

        public List<ReadStoryModel> GetStories()
        {
            IEnumerable<DBStoryModel> dbStories = _storyDB.Stories.Where(i => 
            i.ShareWithFamOnly == false).OrderByDescending(x => x.TimeSubmited);
            List<ReadStoryModel> stories = new();
            foreach (DBStoryModel dbstory in dbStories)
            {
                if(dbstory.ShareAnon)
                {
                    stories.Add(new ReadStoryModel(dbstory.Story, "Anonymous"));
                }
                else
                {
                    stories.Add(new ReadStoryModel(dbstory.Story, dbstory.Name));
                }
            }
            return stories;
        }

        public List<ReadStoryModel> Admin()
        {
            IEnumerable<DBStoryModel> dbStories = _storyDB.Stories.ToList()
                .OrderByDescending(x => x.TimeSubmited);
            List<ReadStoryModel> stories = new();
            foreach (DBStoryModel dbstory in dbStories)
            {
                if(dbstory.ShareWithFamOnly)
                {
                    dbstory.Name += " (Family Only)";
                }
                stories.Add(new ReadStoryModel(dbstory.Story, dbstory.Name));
            }
            return stories;
        }
    }
}
