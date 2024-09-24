using Microsoft.EntityFrameworkCore;
using MTTKDotNetCore.ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTTKDotNetCore.ConsoleApp
{
    public class EFCoreExample
    {
        public void Read()
        {
            AppDbContext db = new AppDbContext();
            var lst = db.Blogs.Where(x => x.DeleteFlag == false).ToList();

            foreach (var item in lst)
            {
                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
            }
        }

        public void Create(string title, string author, string content)
        {
            BlogDataModel model = new BlogDataModel
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };
            AppDbContext db = new AppDbContext();
            db.Blogs.Add(model);
            var result = db.SaveChanges();

            Console.WriteLine(result == 1 ? "Saving Successfully.." : "Saving failed..");
        }

        public void Edit(int id)
        {
            AppDbContext db = new AppDbContext();
            //var result = db.Blogs.Where(x => x.BlogId == id).FirstOrDefault();
            var result = db.Blogs.FirstOrDefault(x => x.BlogId == id);

            if (result == null)
            {
                Console.WriteLine("No data found..");
                return;
            }
            Console.WriteLine(result.BlogId);
            Console.WriteLine(result.BlogTitle);
            Console.WriteLine(result.BlogAuthor);
            Console.WriteLine(result.BlogContent);
        }

        public void Update(int id,  string title, string author, string content)
        {
            AppDbContext db = new AppDbContext();
            var item = db.Blogs
                .AsNoTracking()
                .FirstOrDefault(x => x.BlogId == id);

            if (item is null)
            {
                Console.WriteLine("No data found..");
                return;
            }
            
            if (!string.IsNullOrEmpty(title))
            {
                item.BlogTitle = title;
            }

            if (!string.IsNullOrEmpty(author))
            {
                item.BlogAuthor = author;
            }

            if (!string.IsNullOrEmpty(content))
            {
                item.BlogContent = content;
            }

            db.Entry(item).State = EntityState.Modified; // this include because of AsNoTracking()
            var result = db.SaveChanges();

            Console.WriteLine(result == 1 ? "Updating Successfully.." : "Updating failed..");
        }

        public void Delete(int id)
        {
            AppDbContext db = new AppDbContext();
            var item = db.Blogs
                .AsNoTracking()
                .Where(x => x.BlogId == id).FirstOrDefault();

            if (item is null)
            {
                Console.WriteLine("No Data Found..");
            }
            else if (item != null)
            {
                item.DeleteFlag = true;
            }

            db.Entry(item).State = EntityState.Modified; // This will deleteFlag = 1

            //db.Entry(item).State = EntityState.Deleted; // This will delete
            var result = db.SaveChanges();

            Console.WriteLine(result == 1 ? "Successfully Deleted.." : "Deleting failed..");
        }
    }
}
