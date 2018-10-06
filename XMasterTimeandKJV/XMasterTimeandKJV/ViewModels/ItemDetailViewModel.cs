using System;

using XMasterTimeandKJV.Models;

namespace XMasterTimeandKJV.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public ItemDetailViewModel(Item item = null)
        {
            Title = item?.Text;
            Item = item;
        }
    }  public class WorkInstanceDetailViewModel : BaseViewModel
    {
        public WorkInstance WorkInstance { get; set; }
        public WorkInstanceDetailViewModel(WorkInstance item = null)
        {
            Title = item?.Date.ToString();
            WorkInstance = item;
        }
    }
}
