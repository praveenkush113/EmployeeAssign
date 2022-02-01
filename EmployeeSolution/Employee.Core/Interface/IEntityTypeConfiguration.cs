using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Core.Interface
{
    public interface IEntityTypeConfiguration<TEntityType> where TEntityType : class
    {
      //  void MapEntity(EntityTypeBuilder<TEntityType> builder);
    }
}
