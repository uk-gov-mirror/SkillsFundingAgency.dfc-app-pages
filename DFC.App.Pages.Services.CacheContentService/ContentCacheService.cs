﻿using DFC.App.Pages.Data.Contracts;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DFC.App.Pages.Services.CacheContentService
{
    public class ContentCacheService : IContentCacheService
    {
        private readonly ILogger<ContentCacheService> logger;

        public ContentCacheService(ILogger<ContentCacheService> logger)
        {
            this.logger = logger;
        }

        private IDictionary<Guid, List<Guid>> ContentItems { get; set; } = new Dictionary<Guid, List<Guid>>();

        public bool CheckIsContentItem(Guid contentItemId)
        {
            logger.LogInformation($"{nameof(CheckIsContentItem)} looking for {contentItemId} in {JsonConvert.SerializeObject(ContentItems)}");

            foreach (var contentId in ContentItems.Keys)
            {
                if (ContentItems[contentId].Contains(contentItemId))
                {
                    return true;
                }
            }

            return false;
        }

        public void Clear()
        {
            ContentItems.Clear();
        }

        public IList<Guid> GetContentIdsContainingContentItemId(Guid contentItemId)
        {
            var contentIds = new List<Guid>();

            foreach (var contentId in ContentItems.Keys)
            {
                if (ContentItems[contentId].Contains(contentItemId))
                {
                    contentIds.Add(contentId);
                }
            }

            return contentIds;
        }

        public void Remove(Guid contentId)
        {
            if (ContentItems.ContainsKey(contentId))
            {
                ContentItems.Remove(contentId);
            }
        }

        public void RemoveContentItem(Guid contentId, Guid contentItemId)
        {
            if (ContentItems.ContainsKey(contentId))
            {
                ContentItems[contentId].Remove(contentItemId);
            }
        }

        public void AddOrReplace(Guid contentId, List<Guid> contentItemIds)
        {
            if (ContentItems.ContainsKey(contentId))
            {
                ContentItems[contentId] = contentItemIds;
            }
            else
            {
                ContentItems.Add(contentId, contentItemIds);
            }
        }
    }
}
