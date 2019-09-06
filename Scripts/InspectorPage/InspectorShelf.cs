using System;
using System.Collections.Generic;
using System.Linq;

namespace DUCK.DebugMenu.InspectorPage
{
	public class InspectorShelf
	{
		public class InspectorShelfItem
		{
			public string Name { get; set; }
			public object Object { get; set; }
		}

		public IEnumerable<InspectorShelfItem> Items => shelvedItems;

		private List<InspectorShelfItem> shelvedItems { get; } = new List<InspectorShelfItem>();

		public void Add(object obj, string name = null)
		{
			if (obj == null) throw new ArgumentNullException(nameof(obj));

			var item = new InspectorShelfItem
			{
				Object = obj,
				Name = string.IsNullOrEmpty(name) ? obj.ToString() : name
			};

			shelvedItems.Add(item);
		}

		public void Remove(object obj)
		{
			var matches = shelvedItems.Where(i => i.Object == obj);
			foreach (var match in matches)
			{
				shelvedItems.Remove(match);
			}
		}
	}
}