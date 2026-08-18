using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Security.Permissions
{
	// Token: 0x0200073D RID: 1853
	[SecurityPermission(SecurityAction.InheritanceDemand, ControlEvidence = true, ControlPolicy = true)]
	[Serializable]
	public abstract class ResourcePermissionBase : CodeAccessPermission, IUnrestrictedPermission
	{
		// Token: 0x06003883 RID: 14467 RVA: 0x000EE46C File Offset: 0x000ED46C
		protected ResourcePermissionBase()
		{
		}

		// Token: 0x06003884 RID: 14468 RVA: 0x000EE47F File Offset: 0x000ED47F
		protected ResourcePermissionBase(PermissionState state)
		{
			if (state == PermissionState.Unrestricted)
			{
				this.isUnrestricted = true;
				return;
			}
			if (state == PermissionState.None)
			{
				this.isUnrestricted = false;
				return;
			}
			throw new ArgumentException(SR.GetString("InvalidPermissionState"), "state");
		}

		// Token: 0x06003885 RID: 14469 RVA: 0x000EE4BD File Offset: 0x000ED4BD
		private static Hashtable CreateHashtable()
		{
			return new Hashtable(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x17000D1F RID: 3359
		// (get) Token: 0x06003886 RID: 14470 RVA: 0x000EE4CC File Offset: 0x000ED4CC
		private string ComputerName
		{
			get
			{
				if (ResourcePermissionBase.computerName == null)
				{
					lock (typeof(ResourcePermissionBase))
					{
						if (ResourcePermissionBase.computerName == null)
						{
							StringBuilder stringBuilder = new StringBuilder(256);
							int capacity = stringBuilder.Capacity;
							ResourcePermissionBase.UnsafeNativeMethods.GetComputerName(stringBuilder, ref capacity);
							ResourcePermissionBase.computerName = stringBuilder.ToString();
						}
					}
				}
				return ResourcePermissionBase.computerName;
			}
		}

		// Token: 0x17000D20 RID: 3360
		// (get) Token: 0x06003887 RID: 14471 RVA: 0x000EE53C File Offset: 0x000ED53C
		private bool IsEmpty
		{
			get
			{
				return !this.isUnrestricted && this.rootTable.Count == 0;
			}
		}

		// Token: 0x17000D21 RID: 3361
		// (get) Token: 0x06003888 RID: 14472 RVA: 0x000EE556 File Offset: 0x000ED556
		// (set) Token: 0x06003889 RID: 14473 RVA: 0x000EE55E File Offset: 0x000ED55E
		protected Type PermissionAccessType
		{
			get
			{
				return this.permissionAccessType;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (!value.IsEnum)
				{
					throw new ArgumentException(SR.GetString("PermissionBadParameterEnum"), "value");
				}
				this.permissionAccessType = value;
			}
		}

		// Token: 0x17000D22 RID: 3362
		// (get) Token: 0x0600388A RID: 14474 RVA: 0x000EE592 File Offset: 0x000ED592
		// (set) Token: 0x0600388B RID: 14475 RVA: 0x000EE59C File Offset: 0x000ED59C
		protected string[] TagNames
		{
			get
			{
				return this.tagNames;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Length == 0)
				{
					throw new ArgumentException(SR.GetString("PermissionInvalidLength", new object[]
					{
						"0"
					}), "value");
				}
				this.tagNames = value;
			}
		}

		// Token: 0x0600388C RID: 14476 RVA: 0x000EE5E8 File Offset: 0x000ED5E8
		protected void AddPermissionAccess(ResourcePermissionBaseEntry entry)
		{
			if (entry == null)
			{
				throw new ArgumentNullException("entry");
			}
			if (entry.PermissionAccessPath.Length != this.TagNames.Length)
			{
				throw new InvalidOperationException(SR.GetString("PermissionNumberOfElements"));
			}
			Hashtable hashtable = this.rootTable;
			string[] permissionAccessPath = entry.PermissionAccessPath;
			for (int i = 0; i < permissionAccessPath.Length - 1; i++)
			{
				if (hashtable.ContainsKey(permissionAccessPath[i]))
				{
					hashtable = (Hashtable)hashtable[permissionAccessPath[i]];
				}
				else
				{
					Hashtable hashtable2 = ResourcePermissionBase.CreateHashtable();
					hashtable[permissionAccessPath[i]] = hashtable2;
					hashtable = hashtable2;
				}
			}
			if (hashtable.ContainsKey(permissionAccessPath[permissionAccessPath.Length - 1]))
			{
				throw new InvalidOperationException(SR.GetString("PermissionItemExists"));
			}
			hashtable[permissionAccessPath[permissionAccessPath.Length - 1]] = entry.PermissionAccess;
		}

		// Token: 0x0600388D RID: 14477 RVA: 0x000EE6A8 File Offset: 0x000ED6A8
		protected void Clear()
		{
			this.rootTable.Clear();
		}

		// Token: 0x0600388E RID: 14478 RVA: 0x000EE6B8 File Offset: 0x000ED6B8
		public override IPermission Copy()
		{
			ResourcePermissionBase resourcePermissionBase = this.CreateInstance();
			resourcePermissionBase.tagNames = this.tagNames;
			resourcePermissionBase.permissionAccessType = this.permissionAccessType;
			resourcePermissionBase.isUnrestricted = this.isUnrestricted;
			resourcePermissionBase.rootTable = this.CopyChildren(this.rootTable, 0);
			return resourcePermissionBase;
		}

		// Token: 0x0600388F RID: 14479 RVA: 0x000EE704 File Offset: 0x000ED704
		private Hashtable CopyChildren(object currentContent, int tagIndex)
		{
			IDictionaryEnumerator enumerator = ((Hashtable)currentContent).GetEnumerator();
			Hashtable hashtable = ResourcePermissionBase.CreateHashtable();
			while (enumerator.MoveNext())
			{
				if (tagIndex < this.TagNames.Length - 1)
				{
					hashtable[enumerator.Key] = this.CopyChildren(enumerator.Value, tagIndex + 1);
				}
				else
				{
					hashtable[enumerator.Key] = enumerator.Value;
				}
			}
			return hashtable;
		}

		// Token: 0x06003890 RID: 14480 RVA: 0x000EE76A File Offset: 0x000ED76A
		private ResourcePermissionBase CreateInstance()
		{
			new PermissionSet(PermissionState.Unrestricted).Assert();
			return (ResourcePermissionBase)Activator.CreateInstance(base.GetType(), BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, null, null);
		}

		// Token: 0x06003891 RID: 14481 RVA: 0x000EE78F File Offset: 0x000ED78F
		protected ResourcePermissionBaseEntry[] GetPermissionEntries()
		{
			return this.GetChildrenAccess(this.rootTable, 0);
		}

		// Token: 0x06003892 RID: 14482 RVA: 0x000EE7A0 File Offset: 0x000ED7A0
		private ResourcePermissionBaseEntry[] GetChildrenAccess(object currentContent, int tagIndex)
		{
			IDictionaryEnumerator enumerator = ((Hashtable)currentContent).GetEnumerator();
			ArrayList arrayList = new ArrayList();
			while (enumerator.MoveNext())
			{
				if (tagIndex < this.TagNames.Length - 1)
				{
					ResourcePermissionBaseEntry[] childrenAccess = this.GetChildrenAccess(enumerator.Value, tagIndex + 1);
					for (int i = 0; i < childrenAccess.Length; i++)
					{
						childrenAccess[i].PermissionAccessPath[tagIndex] = (string)enumerator.Key;
					}
					arrayList.AddRange(childrenAccess);
				}
				else
				{
					ResourcePermissionBaseEntry resourcePermissionBaseEntry = new ResourcePermissionBaseEntry((int)enumerator.Value, new string[this.TagNames.Length]);
					resourcePermissionBaseEntry.PermissionAccessPath[tagIndex] = (string)enumerator.Key;
					arrayList.Add(resourcePermissionBaseEntry);
				}
			}
			return (ResourcePermissionBaseEntry[])arrayList.ToArray(typeof(ResourcePermissionBaseEntry));
		}

		// Token: 0x06003893 RID: 14483 RVA: 0x000EE86C File Offset: 0x000ED86C
		public override void FromXml(SecurityElement securityElement)
		{
			if (securityElement == null)
			{
				throw new ArgumentNullException("securityElement");
			}
			if (!securityElement.Tag.Equals("Permission") && !securityElement.Tag.Equals("IPermission"))
			{
				throw new ArgumentException(SR.GetString("Argument_NotAPermissionElement"));
			}
			string text = securityElement.Attribute("version");
			if (text != null && !text.Equals("1"))
			{
				throw new ArgumentException(SR.GetString("Argument_InvalidXMLBadVersion"));
			}
			string text2 = securityElement.Attribute("Unrestricted");
			if (text2 != null && string.Compare(text2, "true", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.isUnrestricted = true;
				return;
			}
			this.isUnrestricted = false;
			this.rootTable = (Hashtable)this.ReadChildren(securityElement, 0);
		}

		// Token: 0x06003894 RID: 14484 RVA: 0x000EE928 File Offset: 0x000ED928
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			if (target.GetType() != base.GetType())
			{
				throw new ArgumentException(SR.GetString("PermissionTypeMismatch"), "target");
			}
			ResourcePermissionBase resourcePermissionBase = (ResourcePermissionBase)target;
			if (this.IsUnrestricted())
			{
				return resourcePermissionBase.Copy();
			}
			if (resourcePermissionBase.IsUnrestricted())
			{
				return this.Copy();
			}
			ResourcePermissionBase resourcePermissionBase2 = null;
			Hashtable hashtable = (Hashtable)this.IntersectContents(this.rootTable, resourcePermissionBase.rootTable);
			if (hashtable != null)
			{
				resourcePermissionBase2 = this.CreateInstance();
				resourcePermissionBase2.rootTable = hashtable;
			}
			return resourcePermissionBase2;
		}

		// Token: 0x06003895 RID: 14485 RVA: 0x000EE9B0 File Offset: 0x000ED9B0
		private object IntersectContents(object currentContent, object targetContent)
		{
			if (currentContent is int)
			{
				int num = (int)currentContent;
				int num2 = (int)targetContent;
				return num & num2;
			}
			Hashtable hashtable = ResourcePermissionBase.CreateHashtable();
			object obj = ((Hashtable)currentContent)["."];
			object obj2 = ((Hashtable)currentContent)[this.ComputerName];
			if (obj != null || obj2 != null)
			{
				object obj3 = ((Hashtable)targetContent)["."];
				object obj4 = ((Hashtable)targetContent)[this.ComputerName];
				if (obj3 != null || obj4 != null)
				{
					object currentContent2 = obj;
					if (obj != null && obj2 != null)
					{
						currentContent2 = this.UnionOfContents(obj, obj2);
					}
					else if (obj2 != null)
					{
						currentContent2 = obj2;
					}
					object targetContent2 = obj3;
					if (obj3 != null && obj4 != null)
					{
						targetContent2 = this.UnionOfContents(obj3, obj4);
					}
					else if (obj4 != null)
					{
						targetContent2 = obj4;
					}
					object value = this.IntersectContents(currentContent2, targetContent2);
					if (this.HasContent(value))
					{
						if (obj2 != null || obj4 != null)
						{
							hashtable[this.ComputerName] = value;
						}
						else
						{
							hashtable["."] = value;
						}
					}
				}
			}
			IDictionaryEnumerator enumerator;
			Hashtable hashtable2;
			if (((Hashtable)currentContent).Count < ((Hashtable)targetContent).Count)
			{
				enumerator = ((Hashtable)currentContent).GetEnumerator();
				hashtable2 = (Hashtable)targetContent;
			}
			else
			{
				enumerator = ((Hashtable)targetContent).GetEnumerator();
				hashtable2 = (Hashtable)currentContent;
			}
			while (enumerator.MoveNext())
			{
				string text = (string)enumerator.Key;
				if (hashtable2.ContainsKey(text) && text != "." && text != this.ComputerName)
				{
					object value2 = enumerator.Value;
					object targetContent3 = hashtable2[text];
					object value3 = this.IntersectContents(value2, targetContent3);
					if (this.HasContent(value3))
					{
						hashtable[text] = value3;
					}
				}
			}
			if (hashtable.Count <= 0)
			{
				return null;
			}
			return hashtable;
		}

		// Token: 0x06003896 RID: 14486 RVA: 0x000EEB84 File Offset: 0x000EDB84
		private bool HasContent(object value)
		{
			return value != null && (!(value is int) || (int)value != 0);
		}

		// Token: 0x06003897 RID: 14487 RVA: 0x000EEBAC File Offset: 0x000EDBAC
		private bool IsContentSubset(object currentContent, object targetContent)
		{
			if (currentContent is int)
			{
				int num = (int)currentContent;
				int num2 = (int)targetContent;
				return (num & num2) == num;
			}
			Hashtable hashtable = (Hashtable)currentContent;
			Hashtable hashtable2 = (Hashtable)targetContent;
			object obj = hashtable2["*"];
			if (obj != null)
			{
				foreach (object obj2 in hashtable)
				{
					if (!this.IsContentSubset(((DictionaryEntry)obj2).Value, obj))
					{
						return false;
					}
				}
				return true;
			}
			foreach (object obj3 in hashtable)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj3;
				string text = (string)dictionaryEntry.Key;
				if (text != "." && text != this.ComputerName)
				{
					if (!hashtable2.ContainsKey(text))
					{
						return false;
					}
					if (!this.IsContentSubset(dictionaryEntry.Value, hashtable2[text]))
					{
						return false;
					}
				}
			}
			object obj4 = this.MergeContents(hashtable["."], hashtable[this.ComputerName]);
			if (obj4 != null)
			{
				object obj5 = this.MergeContents(hashtable2["."], hashtable2[this.ComputerName]);
				if (obj5 != null)
				{
					return this.IsContentSubset(obj4, obj5);
				}
				if (!this.IsEmpty)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003898 RID: 14488 RVA: 0x000EED58 File Offset: 0x000EDD58
		private object MergeContents(object content1, object content2)
		{
			if (content1 == null)
			{
				if (content2 == null)
				{
					return null;
				}
				return content2;
			}
			else
			{
				if (content2 == null)
				{
					return content1;
				}
				return this.UnionOfContents(content1, content2);
			}
		}

		// Token: 0x06003899 RID: 14489 RVA: 0x000EED74 File Offset: 0x000EDD74
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return this.IsEmpty;
			}
			if (target.GetType() != base.GetType())
			{
				return false;
			}
			ResourcePermissionBase resourcePermissionBase = (ResourcePermissionBase)target;
			return resourcePermissionBase.IsUnrestricted() || (!this.IsUnrestricted() && this.IsContentSubset(this.rootTable, resourcePermissionBase.rootTable));
		}

		// Token: 0x0600389A RID: 14490 RVA: 0x000EEDC8 File Offset: 0x000EDDC8
		public bool IsUnrestricted()
		{
			return this.isUnrestricted;
		}

		// Token: 0x0600389B RID: 14491 RVA: 0x000EEDD0 File Offset: 0x000EDDD0
		private object ReadChildren(SecurityElement securityElement, int tagIndex)
		{
			Hashtable hashtable = ResourcePermissionBase.CreateHashtable();
			if (securityElement.Children != null)
			{
				for (int i = 0; i < securityElement.Children.Count; i++)
				{
					SecurityElement securityElement2 = (SecurityElement)securityElement.Children[i];
					if (securityElement2.Tag == this.TagNames[tagIndex])
					{
						string key = securityElement2.Attribute("name");
						if (tagIndex < this.TagNames.Length - 1)
						{
							hashtable[key] = this.ReadChildren(securityElement2, tagIndex + 1);
						}
						else
						{
							string text = securityElement2.Attribute("access");
							int num = 0;
							if (text != null)
							{
								num = (int)Enum.Parse(this.PermissionAccessType, text);
							}
							hashtable[key] = num;
						}
					}
				}
			}
			return hashtable;
		}

		// Token: 0x0600389C RID: 14492 RVA: 0x000EEE94 File Offset: 0x000EDE94
		protected void RemovePermissionAccess(ResourcePermissionBaseEntry entry)
		{
			if (entry == null)
			{
				throw new ArgumentNullException("entry");
			}
			if (entry.PermissionAccessPath.Length != this.TagNames.Length)
			{
				throw new InvalidOperationException(SR.GetString("PermissionNumberOfElements"));
			}
			Hashtable hashtable = this.rootTable;
			string[] permissionAccessPath = entry.PermissionAccessPath;
			for (int i = 0; i < permissionAccessPath.Length; i++)
			{
				if (hashtable == null || !hashtable.ContainsKey(permissionAccessPath[i]))
				{
					throw new InvalidOperationException(SR.GetString("PermissionItemDoesntExist"));
				}
				Hashtable hashtable2 = hashtable;
				if (i < permissionAccessPath.Length - 1)
				{
					hashtable = (Hashtable)hashtable[permissionAccessPath[i]];
					if (hashtable.Count == 1)
					{
						hashtable2.Remove(permissionAccessPath[i]);
					}
				}
				else
				{
					hashtable = null;
					hashtable2.Remove(permissionAccessPath[i]);
				}
			}
		}

		// Token: 0x0600389D RID: 14493 RVA: 0x000EEF44 File Offset: 0x000EDF44
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("IPermission");
			Type type = base.GetType();
			securityElement.AddAttribute("class", type.FullName + ", " + type.Module.Assembly.FullName.Replace('"', '\''));
			securityElement.AddAttribute("version", "1");
			if (this.isUnrestricted)
			{
				securityElement.AddAttribute("Unrestricted", "true");
				return securityElement;
			}
			this.WriteChildren(securityElement, this.rootTable, 0);
			return securityElement;
		}

		// Token: 0x0600389E RID: 14494 RVA: 0x000EEFD0 File Offset: 0x000EDFD0
		public override IPermission Union(IPermission target)
		{
			if (target == null)
			{
				return this.Copy();
			}
			if (target.GetType() != base.GetType())
			{
				throw new ArgumentException(SR.GetString("PermissionTypeMismatch"), "target");
			}
			ResourcePermissionBase resourcePermissionBase = (ResourcePermissionBase)target;
			ResourcePermissionBase resourcePermissionBase2 = null;
			if (this.IsUnrestricted() || resourcePermissionBase.IsUnrestricted())
			{
				resourcePermissionBase2 = this.CreateInstance();
				resourcePermissionBase2.isUnrestricted = true;
			}
			else
			{
				Hashtable hashtable = (Hashtable)this.UnionOfContents(this.rootTable, resourcePermissionBase.rootTable);
				if (hashtable != null)
				{
					resourcePermissionBase2 = this.CreateInstance();
					resourcePermissionBase2.rootTable = hashtable;
				}
			}
			return resourcePermissionBase2;
		}

		// Token: 0x0600389F RID: 14495 RVA: 0x000EF060 File Offset: 0x000EE060
		private object UnionOfContents(object currentContent, object targetContent)
		{
			if (currentContent is int)
			{
				int num = (int)currentContent;
				int num2 = (int)targetContent;
				return num | num2;
			}
			Hashtable hashtable = ResourcePermissionBase.CreateHashtable();
			IDictionaryEnumerator enumerator = ((Hashtable)currentContent).GetEnumerator();
			IDictionaryEnumerator enumerator2 = ((Hashtable)targetContent).GetEnumerator();
			while (enumerator.MoveNext())
			{
				hashtable[(string)enumerator.Key] = enumerator.Value;
			}
			while (enumerator2.MoveNext())
			{
				if (!hashtable.ContainsKey(enumerator2.Key))
				{
					hashtable[enumerator2.Key] = enumerator2.Value;
				}
				else
				{
					object currentContent2 = hashtable[enumerator2.Key];
					object value = enumerator2.Value;
					hashtable[enumerator2.Key] = this.UnionOfContents(currentContent2, value);
				}
			}
			if (hashtable.Count <= 0)
			{
				return null;
			}
			return hashtable;
		}

		// Token: 0x060038A0 RID: 14496 RVA: 0x000EF138 File Offset: 0x000EE138
		private void WriteChildren(SecurityElement currentElement, object currentContent, int tagIndex)
		{
			IDictionaryEnumerator enumerator = ((Hashtable)currentContent).GetEnumerator();
			while (enumerator.MoveNext())
			{
				SecurityElement securityElement = new SecurityElement(this.TagNames[tagIndex]);
				currentElement.AddChild(securityElement);
				securityElement.AddAttribute("name", (string)enumerator.Key);
				if (tagIndex < this.TagNames.Length - 1)
				{
					this.WriteChildren(securityElement, enumerator.Value, tagIndex + 1);
				}
				else
				{
					int num = (int)enumerator.Value;
					if (this.PermissionAccessType != null && num != 0)
					{
						string value = Enum.Format(this.PermissionAccessType, num, "g");
						securityElement.AddAttribute("access", value);
					}
				}
			}
		}

		// Token: 0x0400325A RID: 12890
		public const string Any = "*";

		// Token: 0x0400325B RID: 12891
		public const string Local = ".";

		// Token: 0x0400325C RID: 12892
		private static string computerName;

		// Token: 0x0400325D RID: 12893
		private string[] tagNames;

		// Token: 0x0400325E RID: 12894
		private Type permissionAccessType;

		// Token: 0x0400325F RID: 12895
		private bool isUnrestricted;

		// Token: 0x04003260 RID: 12896
		private Hashtable rootTable = ResourcePermissionBase.CreateHashtable();

		// Token: 0x0200073E RID: 1854
		[SuppressUnmanagedCodeSecurity]
		private static class UnsafeNativeMethods
		{
			// Token: 0x060038A1 RID: 14497
			[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto)]
			internal static extern bool GetComputerName(StringBuilder lpBuffer, ref int nSize);
		}
	}
}
