﻿using System.Collections.Generic;
using System.Web.Mvc;

namespace GigHub.MVC.ViewModels
{
    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<SelectListItem> Providers { get; set; }
    }
}