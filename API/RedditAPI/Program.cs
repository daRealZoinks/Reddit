using Core.Services;
using DataLayer;
using DataLayer.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<AppDbContext>();
builder.Services.AddSingleton<UsersRepository>();
builder.Services.AddSingleton<MessagesRepository>();
builder.Services.AddSingleton<AchievementRepository>();
builder.Services.AddSingleton<CommunityRepository>();
builder.Services.AddSingleton<UnitOfWork>();
builder.Services.AddSingleton<AuthorizationService>();
builder.Services.AddSingleton<IUserCollectionService, UserCollectionService>();
builder.Services.AddSingleton<IMessageCollectionService, MessageCollectionService>();
builder.Services.AddSingleton<IAchievementCollectionService, AchievementCollectionService>();
builder.Services.AddSingleton<ICommunityCollectionService, CommunityCollectionService>();

// Configure authentication
builder.Services.AddAuthentication(options => {
	options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
	options.RequireHttpsMetadata = false;
	options.SaveToken = true;
	options.TokenValidationParameters = new TokenValidationParameters {
		ValidateIssuer = false,
		ValidateAudience = false,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		ClockSkew = TimeSpan.Zero,

		ValidIssuer = "Backend",
		ValidAudience = "Frontend",
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecurityKey"]))
	};
});

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment()) {
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
