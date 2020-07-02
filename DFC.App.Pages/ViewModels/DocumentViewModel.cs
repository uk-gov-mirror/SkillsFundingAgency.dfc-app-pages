﻿using DFC.Compui.Cosmos.Enums;
using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DFC.App.Pages.ViewModels
{
    public class DocumentViewModel
    {
        public HtmlHeadViewModel? HtmlHead { get; set; }

        public BreadcrumbViewModel? Breadcrumb { get; set; }

        [Display(Name = "Document Id")]
        public Guid? DocumentId { get; set; }

        [Display(Name = "Canonical Name")]
        public string? CanonicalName { get; set; }

        [Display(Name = "PartitionKey")]
        public string? PartitionKey { get; set; }

        [Required]
        public Guid? Version { get; set; }

        [Display(Name = "Breadcrumb Title")]
        public string? BreadcrumbTitle { get; set; }

        [Display(Name = "Include In SiteMap")]
        public bool IncludeInSitemap { get; set; }

        [Display(Name = "SiteMap Priority")]
        public decimal SiteMapPriority { get; set; }

        [Display(Name = "SiteMap Change Frequency")]
        public SiteMapChangeFrequency SiteMapChangeFrequency { get; set; }

        public Uri? Url { get; set; }

        public HtmlString? Content { get; set; }

        [Display(Name = "Last Reviewed")]
        public DateTime LastReviewed { get; set; }

        public IList<string>? Redirects { get; set; }

        public BodyViewModel? BodyViewModel { get; set; }
    }
}
