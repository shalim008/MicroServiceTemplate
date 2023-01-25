using MasterDataManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterDataManagement.Core.Specifications
{

    public class OwnerListSpecification : BaseSpecification<SysOwner>
    {
        public OwnerListSpecification(OwnerListSpecificationParams postParrams) : base(x =>

                         string.IsNullOrEmpty(postParrams.Search) && x.DataStatus == 1
                    )
        {
            //AddInclude(ob => ob.table);

            AddOrderByDescending(x => x.Id);
            ApplyPaging(postParrams.PageSize * (postParrams.PageIndex - 1), postParrams.PageSize);

            if (!string.IsNullOrEmpty(postParrams.Sort))
            {
                switch (postParrams.Sort)
                {
                    case "ownerCodeNoAsc":
                        AddOrderBy(p => p.OwnerCode);
                        break;
                    case "ownerCodeDesc":
                        AddOrderByDescending(p => p.OwnerCode);
                        break;

                    default:
                        break;
                }
            }
        }

    }
}
