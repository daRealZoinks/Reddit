using DataLayer.Repositories;

namespace DataLayer;

public class UnitOfWork
{
    private readonly AppDbContext _appDbContext;

	public UnitOfWork(AppDbContext appDbContext, UsersRepository usersRepository, MessagesRepository messagesRepository,
		AchievementRepository achievementRepository, CommunityRepository communityRepository,
		PostsRepository postsRepository, CommentsRepository commentsRepository) {
		_appDbContext = appDbContext;
		UsersRepository = usersRepository;
		MessagesRepository = messagesRepository;
		AchievementRepository = achievementRepository;
		CommunityRepository = communityRepository;
		PostsRepository = postsRepository;
		CommentsRepository = commentsRepository;
	}

	public UsersRepository UsersRepository {
		get;
	}
	public MessagesRepository MessagesRepository {
		get;
	}
	public AchievementRepository AchievementRepository {
		get;
	}
	public CommunityRepository CommunityRepository {
		get;
	}
	public PostsRepository PostsRepository {
		get;
	}
	public CommentsRepository CommentsRepository {
		get;
	}

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