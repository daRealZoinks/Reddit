﻿using DataLayer.Repositories;

namespace DataLayer;

public class UnitOfWork
{
    private readonly AppDbContext _appDbContext;

    public UnitOfWork(AppDbContext appDbContext, UsersRepository usersRepository)
    {
        _appDbContext = appDbContext;
        UsersRepository = usersRepository;
    }

    public UsersRepository UsersRepository { get; }

    public void SaveChanges()
    {
        try
        {
            _appDbContext.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error saving changes to database:");
            Console.WriteLine();
            Console.WriteLine(e.Message);
            Console.WriteLine();
            Console.WriteLine(e.InnerException);
            Console.WriteLine();
            Console.WriteLine(e.StackTrace);
        }
    }
}