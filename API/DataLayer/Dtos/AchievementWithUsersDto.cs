﻿namespace DataLayer.Dtos
{
    public class AchievementWithUsersDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public int Value { get; set; }

        public List<UserDto> Users { get; set; } = new();
    }
}