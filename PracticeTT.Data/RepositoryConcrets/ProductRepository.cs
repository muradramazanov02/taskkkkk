using PracticeTT.Core.Abstract;
using PracticeTT.Core.Models;
using PracticeTT.Core.RepositoryAbstract;
using PracticeTT.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeTT.Data.RepositoryConcrets;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}
