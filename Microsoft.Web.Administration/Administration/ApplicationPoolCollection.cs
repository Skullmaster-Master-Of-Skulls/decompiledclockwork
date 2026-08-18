using System;
using System.Globalization;
using System.Text;

namespace Microsoft.Web.Administration
{
	// Token: 0x0200000B RID: 11
	public sealed class ApplicationPoolCollection : ConfigurationElementCollectionBase<ApplicationPool>
	{
		// Token: 0x06000093 RID: 147 RVA: 0x00003A68 File Offset: 0x00002A68
		internal ApplicationPoolCollection(ServerManager owner)
		{
			this._owner = owner;
		}

		// Token: 0x1700004E RID: 78
		public ApplicationPool this[string name]
		{
			get
			{
				return base.FindElementWithCollectionKey("name", name);
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003A88 File Offset: 0x00002A88
		public ApplicationPool Add(string name)
		{
			ApplicationPoolCollection.ValidateName(name);
			ApplicationPool applicationPool = base.CreateElement();
			applicationPool["name"] = name;
			base.Add(applicationPool);
			return applicationPool;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003AB7 File Offset: 0x00002AB7
		protected override ApplicationPool CreateNewElement(string elementTagName)
		{
			return new ApplicationPool(this._owner);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003AC4 File Offset: 0x00002AC4
		public static char[] InvalidApplicationPoolNameCharacters()
		{
			return SharedGlobals.GetInvalidApplicationPoolNameCharacters();
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003ACC File Offset: 0x00002ACC
		private static void ValidateName(string name)
		{
			string text = null;
			if (string.IsNullOrEmpty(name))
			{
				text = Resources.ApplicationPoolNameLengthValidation;
			}
			else
			{
				char[] array = ApplicationPoolCollection.InvalidApplicationPoolNameCharacters();
				if (name.IndexOfAny(array) != -1)
				{
					StringBuilder stringBuilder = new StringBuilder();
					for (int i = 0; i < array.Length; i++)
					{
						stringBuilder.Append(array[i]);
						if (i < array.Length - 1)
						{
							stringBuilder.Append(", ");
						}
					}
					text = string.Format(CultureInfo.InvariantCulture, Resources.ApplicationPoolNameCannotContainChars, new object[]
					{
						stringBuilder.ToString()
					});
				}
				if (name.Trim().Length < 1 || name.Trim().Length > 64)
				{
					text = Resources.ApplicationPoolNameLengthValidation;
				}
			}
			if (!string.IsNullOrEmpty(text))
			{
				throw new FormatException(text);
			}
		}

		// Token: 0x04000023 RID: 35
		private ServerManager _owner;
	}
}
