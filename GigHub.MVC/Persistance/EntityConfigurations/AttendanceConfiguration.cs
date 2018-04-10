﻿using GigHub.MVC.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace GigHub.MVC.Persistance.EntityConfigurations
{
    public class AttendanceConfiguration : EntityTypeConfiguration<Attendance>
    {
        public AttendanceConfiguration()
        {
            HasKey(a => new { a.GigId, a.AttendeeId });
        }
    }
}