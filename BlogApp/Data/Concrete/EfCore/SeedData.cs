using BlogApp.Entity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data.Concrete.EfCore
{
    public static class SeedData
    {
        public static void TestVerileriniDoldu(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<BlogContext>();


            if (context != null)
            {
                if(context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }

                if (!context.Tags.Any())
                {
                    context.Tags.AddRange(
                        new Entity.Tag{Text="web programlama", Url="web-programlama"},
                        new Entity.Tag{Text="backend", Url="backend"},
                        new Entity.Tag{Text="frontend", Url="frontend"},
                        new Entity.Tag{Text="fullstack", Url="fullstack"},
                        new Entity.Tag{Text="php", Url="php"}
                    );
                    context.SaveChanges();
                }

                if (!context.Users.Any())
                {
                    context.Users.AddRange(
                        new Entity.User{UserName="bulenttopcu",Name="Bülent Topçu",Email="bulenttopcu@gmail.com",Password="123456", Image="p1.jpg"},
                        new Entity.User{UserName="tolgatopcu",Name="Tolga Topçu",Email="tolgatopcu@gmail.com",Password="123456",Image="p2.jpg"}

                    );
                    context.SaveChanges();
                }

                if (!context.Posts.Any())
                {
                    context.Posts.AddRange(
                        new Entity.Post{
                        Title="Asp .net core",
                        Content="aspnet core dersleri",
                        Description="aspnet core dersleri",
                        Url="aspnet-core",
                        IsActive=true,
                        PublishedOn=DateTime.Now.AddDays(-10),
                        Tags=context.Tags.Take(3).ToList(),
                        Image="1.jpg",
                        UserId=1,
                        Comments=new List<Comment>{
                             new Comment{Text="Harika bir kurs",PublishedOn=DateTime.Now.AddDays(-10),UserId=1},
                             new Comment{Text="Müthiş bir kurs",PublishedOn=DateTime.Now.AddDays(-20),UserId=2},
                             }
                        },
                        new Entity.Post{
                        Title="Php",
                        Content="Php core dersleri",
                        Description="Php core dersleri",
                        Url="php",
                        IsActive=true,
                        PublishedOn=DateTime.Now.AddDays(-5),
                        Tags=context.Tags.Take(2).ToList(),
                        Image="2.jpg",
                        UserId=1
                        },
                        new Entity.Post{
                        Title="Django",
                        Content="Django dersleri",
                        Description="Django dersleri",
                        Url="django",
                        IsActive=true,
                        PublishedOn=DateTime.Now.AddDays(-30),
                        Tags=context.Tags.Take(4).ToList(),
                        Image="3.jpg",
                        UserId=2
                        },
                        new Entity.Post{
                        Title="React",
                        Content="React dersleri",
                        Description="React dersleri",
                        Url="react-dersleri",
                        IsActive=true,
                        PublishedOn=DateTime.Now.AddDays(-40),
                        Tags=context.Tags.Take(4).ToList(),
                        Image="3.jpg",
                        UserId=2
                        },
                        new Entity.Post{
                        Title="Angular",
                        Content="Angular dersleri",
                        Description="Angular dersleri",
                        Url="angular-dersleri",
                        IsActive=true,
                        PublishedOn=DateTime.Now.AddDays(-50),
                        Tags=context.Tags.Take(4).ToList(),
                        Image="3.jpg",
                        UserId=2
                        }

                    );
                    context.SaveChanges();
                }
            }
        }
    }
}