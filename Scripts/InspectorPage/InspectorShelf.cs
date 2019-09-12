using System;
using System.Collections.Generic;
using System.Linq;

namespace DUCK.DebugMenu.InspectorPage
{
	/// <summary>
	/// InspectorShelf is a repository where you can place arbitrary objects that then become accessible to the debug inspector.
	/// </summary>
	public class InspectorShelf
	{
		public class InspectorShelfItem
		{
			public string Name { get; set; }
			public object Object { get; set; }
		}

		public IEnumerable<InspectorShelfItem> Items => shelvedItems;

		private List<InspectorShelfItem> shelvedItems { get; } = new List<InspectorShelfItem>();

		/// <summary>
		/// Adds an object to the shelf with an optional name
		/// </summary>
		/// <param name="obj">The object to add to the shelf</param>
		/// <param name="name">The optional name. If none is provided the object's ToString() function will be used</param>
		/// <exception cref="ArgumentNullException">Throws if the obj argument is null</exception>
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

		/// <summary>
		/// Removes the given object from the shelf
		/// </summary>
		/// <param name="obj"></param>
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