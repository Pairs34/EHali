using System;

namespace HaliciPatron.Views.Model
{
    internal class MenuModel
    {
        public int Id { get; set; }
        public string Icon { get; set; }
        public string Name { get; set; }
        public Type TargetType { get; set; }
    }
}