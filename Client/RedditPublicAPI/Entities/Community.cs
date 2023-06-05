﻿namespace RedditPublicAPI.Entities;

public class Community : BaseEntity
{
    public string Name { get; set; }

    public string Description { get; set; }

    public int ModeratorId { get; set; }

    public User? Moderator { get; set; }

    public List<User> Users { get; set; } = new();
}