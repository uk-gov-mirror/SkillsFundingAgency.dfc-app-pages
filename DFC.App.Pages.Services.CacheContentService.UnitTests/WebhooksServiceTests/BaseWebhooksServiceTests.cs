﻿using DFC.App.Pages.Data.Contracts;
using DFC.App.Pages.Data.Models;
using DFC.App.Pages.Data.Models.CmsApiModels;
using DFC.Compui.Cosmos.Contracts;
using DFC.Content.Pkg.Netcore.Data.Contracts;
using DFC.Content.Pkg.Netcore.Data.Models;
using FakeItEasy;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace DFC.App.Pages.Services.CacheContentService.UnitTests.WebhooksServiceTests
{
    public abstract class BaseWebhooksServiceTests
    {
        protected const string EventTypePublished = "published";
        protected const string EventTypeDraft = "draft";
        protected const string EventTypeDraftDiscarded = "draft-discarded";
        protected const string EventTypeDeleted = "deleted";
        protected const string EventTypeUnpublished = "unpublished";

        protected BaseWebhooksServiceTests()
        {
            Logger = A.Fake<ILogger<WebhooksService>>();
            FakeMapper = A.Fake<AutoMapper.IMapper>();
            FakeEventMessageService = A.Fake<IEventMessageService<ContentPageModel>>();
            FakeCmsApiService = A.Fake<ICmsApiService>();
            FakeContentPageService = A.Fake<IContentPageService<ContentPageModel>>();
            FakeContentCacheService = A.Fake<IContentCacheService>();
            FakeEventGridService = A.Fake<IEventGridService>();
        }

        protected Guid ContentIdForCreate { get; } = Guid.NewGuid();

        protected Guid ContentIdForUpdate { get; } = Guid.NewGuid();

        protected Guid ContentIdForDelete { get; } = Guid.NewGuid();

        protected Guid PageLocationIdForCreate { get; } = Guid.NewGuid();

        protected Guid PageLocationIdForUpdate { get; } = Guid.NewGuid();

        protected Guid ContentItemIdForCreate { get; } = Guid.NewGuid();

        protected Guid ContentItemIdForUpdate { get; } = Guid.NewGuid();

        protected Guid ContentItemIdForDelete { get; } = Guid.NewGuid();

        protected ILogger<WebhooksService> Logger { get; }

        protected AutoMapper.IMapper FakeMapper { get; }

        protected IEventMessageService<ContentPageModel> FakeEventMessageService { get; }

        protected ICmsApiService FakeCmsApiService { get; }

        protected IContentPageService<ContentPageModel> FakeContentPageService { get; }

        protected IContentCacheService FakeContentCacheService { get; }

        protected IEventGridService FakeEventGridService { get; }

        protected static CmsApiPageLocationModel BuildValidPagesApiPageLocationModel(Guid contentItemId)
        {
            var model = new CmsApiPageLocationModel
            {
                ItemId = contentItemId,
                Title = "breadcrump-link",
                BreadcrumbText = "Breadcrumb Text",
            };

            return model;
        }

        protected static ContentItemModel BuildValidContentItemModel(Guid contentItemId, string? contentType = null)
        {
            var model = new ContentItemModel()
            {
                ItemId = contentItemId,
                LastReviewed = DateTime.Now,
                ContentType = contentType,
            };

            return model;
        }

        protected static PageLocationModel BuildValidPagesPageLocationModel(Guid contentItemId)
        {
            var model = new PageLocationModel
            {
                ItemId = contentItemId,
                BreadcrumbLinkSegment = "breadcrump-link",
                BreadcrumbText = "Breadcrumb Text",
            };

            return model;
        }

        protected CmsApiDataModel BuildValidPagesApiContentModel()
        {
            var model = new CmsApiDataModel
            {
                ItemId = Guid.NewGuid(),
                CanonicalName = "an-article",
                ExcludeFromSitemap = true,
                Version = Guid.NewGuid(),
                Url = new Uri("https://localhost"),
                ContentLinks = new ContentLinksModel(new JObject())
                {
                    ContentLinks = new List<KeyValuePair<string, List<LinkDetails>>>()
                    {
                        new KeyValuePair<string, List<LinkDetails>>(
                            "test",
                            new List<LinkDetails>
                            {
                                new LinkDetails
                                {
                                    Uri = new Uri("http://www.one.com"),
                                },
                                new LinkDetails
                                {
                                    Uri = new Uri("http://www.two.com"),
                                },
                                new LinkDetails
                                {
                                    Uri = new Uri("http://www.three.com"),
                                },
                            }),
                    },
                },
                ContentItems = new List<IBaseContentItemModel>
                {
                    BuildValidPagesApiPageLocationModel(PageLocationIdForUpdate),
                },
                Published = DateTime.UtcNow,
            };

            return model;
        }

        protected ContentPageModel BuildValidContentPageModel(string? contentType = null)
        {
            var model = new ContentPageModel()
            {
                Id = ContentIdForUpdate,
                Etag = Guid.NewGuid().ToString(),
                CanonicalName = "an-article",
                IncludeInSitemap = true,
                Version = Guid.NewGuid(),
                Url = new Uri("https://localhost"),
                Content = null,
                ContentItems = new List<ContentItemModel>
                {
                    BuildValidContentItemModel(ContentItemIdForCreate),
                    BuildValidContentItemModel(ContentItemIdForUpdate, contentType),
                    BuildValidContentItemModel(ContentItemIdForDelete),
                },
                PageLocations = new List<PageLocationModel>
                {
                    BuildValidPagesPageLocationModel(PageLocationIdForCreate),
                    BuildValidPagesPageLocationModel(PageLocationIdForUpdate),
                },
                LastReviewed = DateTime.UtcNow,
            };

            return model;
        }

        protected WebhooksService BuildWebhooksService()
        {
            var service = new WebhooksService(Logger, FakeMapper, FakeEventMessageService, FakeCmsApiService, FakeContentPageService, FakeContentCacheService, FakeEventGridService);

            return service;
        }
    }
}