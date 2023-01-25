using MasterDataManagement.Core.Entities;

namespace MasterDataManagement.Core.Specifications
{

    public class OwnerListWithFiltersForCountSpecificication : BaseSpecification<SysOwner>
    {
        public OwnerListWithFiltersForCountSpecificication(OwnerListSpecificationParams postParrams)
           : base(x =>
               string.IsNullOrEmpty(postParrams.Search) && x.DataStatus == 1
           )
        {
            //AddInclude(ob => ob.DcmCaseListCustomer);         
            AddOrderByDescending(x => x.Id);
        }

        public OwnerListWithFiltersForCountSpecificication(long id)
          : base(x =>
              x.Id == id && x.DataStatus == 1
          )
        {
            AddOrderByDescending(x => x.Id);
        }
    }
}
