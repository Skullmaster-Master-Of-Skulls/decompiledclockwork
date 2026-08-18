using System;
using System.Runtime.InteropServices;

namespace System.DirectoryServices.Protocols
{
	// Token: 0x02000029 RID: 41
	public class SortRequestControl : DirectoryControl
	{
		// Token: 0x060000B5 RID: 181 RVA: 0x0000489C File Offset: 0x0000389C
		public SortRequestControl(params SortKey[] sortKeys) : base("1.2.840.113556.1.4.473", null, true, true)
		{
			if (sortKeys == null)
			{
				throw new ArgumentNullException("sortKeys");
			}
			for (int i = 0; i < sortKeys.Length; i++)
			{
				if (sortKeys[i] == null)
				{
					throw new ArgumentException(Res.GetString("NullValueArray"), "sortKeys");
				}
			}
			this.keys = new SortKey[sortKeys.Length];
			for (int j = 0; j < sortKeys.Length; j++)
			{
				this.keys[j] = new SortKey(sortKeys[j].AttributeName, sortKeys[j].MatchingRule, sortKeys[j].ReverseOrder);
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x0000493A File Offset: 0x0000393A
		public SortRequestControl(string attributeName, bool reverseOrder) : this(attributeName, null, reverseOrder)
		{
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00004948 File Offset: 0x00003948
		public SortRequestControl(string attributeName, string matchingRule, bool reverseOrder) : base("1.2.840.113556.1.4.473", null, true, true)
		{
			SortKey sortKey = new SortKey(attributeName, matchingRule, reverseOrder);
			this.keys = new SortKey[1];
			this.keys[0] = sortKey;
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00004990 File Offset: 0x00003990
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x00004A00 File Offset: 0x00003A00
		public SortKey[] SortKeys
		{
			get
			{
				if (this.keys == null)
				{
					return new SortKey[0];
				}
				SortKey[] array = new SortKey[this.keys.Length];
				for (int i = 0; i < this.keys.Length; i++)
				{
					array[i] = new SortKey(this.keys[i].AttributeName, this.keys[i].MatchingRule, this.keys[i].ReverseOrder);
				}
				return array;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				for (int i = 0; i < value.Length; i++)
				{
					if (value[i] == null)
					{
						throw new ArgumentException(Res.GetString("NullValueArray"), "value");
					}
				}
				this.keys = new SortKey[value.Length];
				for (int j = 0; j < value.Length; j++)
				{
					this.keys[j] = new SortKey(value[j].AttributeName, value[j].MatchingRule, value[j].ReverseOrder);
				}
			}
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00004A84 File Offset: 0x00003A84
		public override byte[] GetValue()
		{
			IntPtr intPtr = (IntPtr)0;
			int cb = Marshal.SizeOf(typeof(SortKey));
			int num = this.keys.Length;
			IntPtr intPtr2 = Utility.AllocHGlobalIntPtrArray(num + 1);
			try
			{
				IntPtr ptr = (IntPtr)0;
				IntPtr intPtr3 = (IntPtr)0;
				int i;
				for (i = 0; i < num; i++)
				{
					intPtr3 = Marshal.AllocHGlobal(cb);
					Marshal.StructureToPtr(this.keys[i], intPtr3, false);
					ptr = (IntPtr)((long)intPtr2 + (long)(Marshal.SizeOf(typeof(IntPtr)) * i));
					Marshal.WriteIntPtr(ptr, intPtr3);
				}
				ptr = (IntPtr)((long)intPtr2 + (long)(Marshal.SizeOf(typeof(IntPtr)) * i));
				Marshal.WriteIntPtr(ptr, (IntPtr)0);
				bool isCritical = base.IsCritical;
				int num2 = Wldap32.ldap_create_sort_control(UtilityHandle.GetHandle(), intPtr2, isCritical ? 1 : 0, ref intPtr);
				if (num2 != 0)
				{
					if (Utility.IsLdapError((LdapError)num2))
					{
						string message = LdapErrorMappings.MapResultCode(num2);
						throw new LdapException(num2, message);
					}
					throw new LdapException(num2);
				}
				else
				{
					LdapControl ldapControl = new LdapControl();
					Marshal.PtrToStructure(intPtr, ldapControl);
					berval ldctl_value = ldapControl.ldctl_value;
					this.directoryControlValue = null;
					if (ldctl_value != null)
					{
						this.directoryControlValue = new byte[ldctl_value.bv_len];
						Marshal.Copy(ldctl_value.bv_val, this.directoryControlValue, 0, ldctl_value.bv_len);
					}
				}
			}
			finally
			{
				if (intPtr != (IntPtr)0)
				{
					Wldap32.ldap_control_free(intPtr);
				}
				if (intPtr2 != (IntPtr)0)
				{
					for (int j = 0; j < num; j++)
					{
						IntPtr intPtr4 = Marshal.ReadIntPtr(intPtr2, Marshal.SizeOf(typeof(IntPtr)) * j);
						if (intPtr4 != (IntPtr)0)
						{
							IntPtr intPtr5 = Marshal.ReadIntPtr(intPtr4);
							if (intPtr5 != (IntPtr)0)
							{
								Marshal.FreeHGlobal(intPtr5);
							}
							intPtr5 = Marshal.ReadIntPtr(intPtr4, Marshal.SizeOf(typeof(IntPtr)));
							if (intPtr5 != (IntPtr)0)
							{
								Marshal.FreeHGlobal(intPtr5);
							}
							Marshal.FreeHGlobal(intPtr4);
						}
					}
					Marshal.FreeHGlobal(intPtr2);
				}
			}
			return base.GetValue();
		}

		// Token: 0x040000F3 RID: 243
		private SortKey[] keys = new SortKey[0];
	}
}
