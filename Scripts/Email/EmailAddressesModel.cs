using UnityEngine;

namespace DUCK.DebugMenu.Email
{
	public class EmailAddressesModel
	{
		public string[] Emails { get { return emails; } }

		[SerializeField]
		private string[] emails;
	}
}
