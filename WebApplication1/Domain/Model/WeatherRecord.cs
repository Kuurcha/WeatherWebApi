﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using WebWeatherApi.Domain.ModelConfiguration;

namespace WebWeatherApi.Entities.Model
{
    [Table(nameof(WeatherRecord))]
    [EntityTypeConfiguration(typeof(WeatherRecordConfiguration))]
    public class WeatherRecord
    {
        public WeatherRecord(int id, string description)
        {
            Id = id;
            Description = description;
        }

        public int Id { get; set; }

        public string Description { get; set; }

        public ICollection<WeatherDetails> WeatherDetails { get; set; }
    }
}
