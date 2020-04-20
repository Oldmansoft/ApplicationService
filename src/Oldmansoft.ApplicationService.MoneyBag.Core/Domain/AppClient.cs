using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.ApplicationService.MoneyBag.Domain
{
    public class AppClient
    {
        public Guid Id { get; private set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 注释
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Created { get; private set; }

        private AppClient() { }

        public static AppClient Create(string name, string description)
        {
            var result = new AppClient();
            result.Name = name;
            result.Description = description;
            result.Created = DateTime.UtcNow;
            return result;
        }

        public static AppClient Create(Guid id, string name, string description)
        {
            var result = new AppClient();
            result.Id = id;
            result.Name = name;
            result.Description = description;
            result.Created = DateTime.UtcNow;
            return result;
        }

        public void Change(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
