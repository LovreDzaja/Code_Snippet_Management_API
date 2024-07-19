using Microsoft.EntityFrameworkCore;
using Code_Snippet_Management_API.Models;

namespace Code_Snippet_Management_API.Data
{
    public class ApiContext: DbContext
    {
        public DbSet<CodeSnippet> Snippets { get; set; }
        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {

        }

    }

}
