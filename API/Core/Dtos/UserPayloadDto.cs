﻿using DataLayer.Enums;

namespace Core.Dtos;

public class UserPayloadDto
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public DateTime AccountCreationDate { get; set; }
    public string Description { get; set; }
    public Role Role { get; set; }
}