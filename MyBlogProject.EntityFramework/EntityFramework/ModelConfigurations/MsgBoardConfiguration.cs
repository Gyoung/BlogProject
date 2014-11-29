using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBlogProject.Entity;

namespace MyBlogProject.DataCore.EntityFramework.ModelConfigurations
{
    public class MsgBoardConfiguration : EntityTypeConfiguration<MsgBoard>
    {
        public MsgBoardConfiguration()
        {
            HasKey(k => k.MsgBoardId);
            Property(p => p.MsgBoardId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.IpAddress).HasMaxLength(20);
            Property(p => p.Content).HasMaxLength(500);
            Property(p => p.UserName).HasMaxLength(20);
            Property(p => p.CreateTime);
            Property(p => p.ModifyTime);
            Property(p => p.IsDelete);
        }
    }
}
