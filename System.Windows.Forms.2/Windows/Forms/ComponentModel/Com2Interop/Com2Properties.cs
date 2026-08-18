using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace System.Windows.Forms.ComponentModel.Com2Interop
{
	// Token: 0x020004A2 RID: 1186
	internal class Com2Properties
	{
		// Token: 0x1400040B RID: 1035
		// (add) Token: 0x06004ECC RID: 20172 RVA: 0x00144818 File Offset: 0x00142A18
		// (remove) Token: 0x06004ECD RID: 20173 RVA: 0x00144850 File Offset: 0x00142A50
		public event EventHandler Disposed;

		// Token: 0x06004ECE RID: 20174 RVA: 0x00144888 File Offset: 0x00142A88
		public Com2Properties(object obj, Com2PropertyDescriptor[] props, int defaultIndex)
		{
			this.SetProps(props);
			this.weakObjRef = new WeakReference(obj);
			this.defaultIndex = defaultIndex;
			this.typeInfoVersions = this.GetTypeInfoVersions(obj);
			this.touchedTime = DateTime.Now.Ticks;
		}

		// Token: 0x17001354 RID: 4948
		// (get) Token: 0x06004ECF RID: 20175 RVA: 0x001448DC File Offset: 0x00142ADC
		// (set) Token: 0x06004ED0 RID: 20176 RVA: 0x001448E7 File Offset: 0x00142AE7
		internal bool AlwaysValid
		{
			get
			{
				return this.alwaysValid > 0;
			}
			set
			{
				if (!value)
				{
					if (this.alwaysValid > 0)
					{
						this.alwaysValid--;
					}
					return;
				}
				if (this.alwaysValid == 0 && !this.CheckValid())
				{
					return;
				}
				this.alwaysValid++;
			}
		}

		// Token: 0x17001355 RID: 4949
		// (get) Token: 0x06004ED1 RID: 20177 RVA: 0x00144923 File Offset: 0x00142B23
		public Com2PropertyDescriptor DefaultProperty
		{
			get
			{
				if (!this.CheckValid(true))
				{
					return null;
				}
				if (this.defaultIndex != -1)
				{
					return this.props[this.defaultIndex];
				}
				if (this.props.Length != 0)
				{
					return this.props[0];
				}
				return null;
			}
		}

		// Token: 0x17001356 RID: 4950
		// (get) Token: 0x06004ED2 RID: 20178 RVA: 0x0014495A File Offset: 0x00142B5A
		public object TargetObject
		{
			get
			{
				if (!this.CheckValid(false) || this.touchedTime == 0L)
				{
					return null;
				}
				return this.weakObjRef.Target;
			}
		}

		// Token: 0x17001357 RID: 4951
		// (get) Token: 0x06004ED3 RID: 20179 RVA: 0x0014497C File Offset: 0x00142B7C
		public long TicksSinceTouched
		{
			get
			{
				if (this.touchedTime == 0L)
				{
					return 0L;
				}
				return DateTime.Now.Ticks - this.touchedTime;
			}
		}

		// Token: 0x17001358 RID: 4952
		// (get) Token: 0x06004ED4 RID: 20180 RVA: 0x001449A8 File Offset: 0x00142BA8
		public Com2PropertyDescriptor[] Properties
		{
			get
			{
				this.CheckValid(true);
				if (this.touchedTime == 0L || this.props == null)
				{
					return null;
				}
				this.touchedTime = DateTime.Now.Ticks;
				for (int i = 0; i < this.props.Length; i++)
				{
					this.props[i].SetNeedsRefresh(255, true);
				}
				return this.props;
			}
		}

		// Token: 0x17001359 RID: 4953
		// (get) Token: 0x06004ED5 RID: 20181 RVA: 0x00144A0E File Offset: 0x00142C0E
		public bool TooOld
		{
			get
			{
				this.CheckValid(false, false);
				return this.touchedTime != 0L && this.TicksSinceTouched > Com2Properties.AGE_THRESHHOLD;
			}
		}

		// Token: 0x06004ED6 RID: 20182 RVA: 0x00144A30 File Offset: 0x00142C30
		public void AddExtendedBrowsingHandlers(Hashtable handlers)
		{
			object targetObject = this.TargetObject;
			if (targetObject == null)
			{
				return;
			}
			for (int i = 0; i < Com2Properties.extendedInterfaces.Length; i++)
			{
				Type type = Com2Properties.extendedInterfaces[i];
				if (type.IsInstanceOfType(targetObject))
				{
					Com2ExtendedBrowsingHandler com2ExtendedBrowsingHandler = (Com2ExtendedBrowsingHandler)handlers[type];
					if (com2ExtendedBrowsingHandler == null)
					{
						com2ExtendedBrowsingHandler = (Com2ExtendedBrowsingHandler)Activator.CreateInstance(Com2Properties.extendedInterfaceHandlerTypes[i]);
						handlers[type] = com2ExtendedBrowsingHandler;
					}
					if (!type.IsAssignableFrom(com2ExtendedBrowsingHandler.Interface))
					{
						throw new ArgumentException(SR.GetString("COM2BadHandlerType", new object[]
						{
							type.Name,
							com2ExtendedBrowsingHandler.Interface.Name
						}));
					}
					com2ExtendedBrowsingHandler.SetupPropertyHandlers(this.props);
				}
			}
		}

		// Token: 0x06004ED7 RID: 20183 RVA: 0x00144AE4 File Offset: 0x00142CE4
		public void Dispose()
		{
			if (this.props != null)
			{
				if (this.Disposed != null)
				{
					this.Disposed(this, EventArgs.Empty);
				}
				this.weakObjRef = null;
				this.props = null;
				this.touchedTime = 0L;
			}
		}

		// Token: 0x06004ED8 RID: 20184 RVA: 0x00144B1D File Offset: 0x00142D1D
		public bool CheckValid()
		{
			return this.CheckValid(false);
		}

		// Token: 0x06004ED9 RID: 20185 RVA: 0x00144B26 File Offset: 0x00142D26
		public bool CheckValid(bool checkVersions)
		{
			return this.CheckValid(checkVersions, true);
		}

		// Token: 0x06004EDA RID: 20186 RVA: 0x00144B30 File Offset: 0x00142D30
		private long[] GetTypeInfoVersions(object comObject)
		{
			UnsafeNativeMethods.ITypeInfo[] array = Com2TypeInfoProcessor.FindTypeInfos(comObject, false);
			long[] array2 = new long[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = this.GetTypeInfoVersion(array[i]);
			}
			return array2;
		}

		// Token: 0x1700135A RID: 4954
		// (get) Token: 0x06004EDB RID: 20187 RVA: 0x00144B69 File Offset: 0x00142D69
		private static int CountMemberOffset
		{
			get
			{
				if (Com2Properties.countOffset == -1)
				{
					Com2Properties.countOffset = Marshal.SizeOf(typeof(Guid)) + IntPtr.Size + 24;
				}
				return Com2Properties.countOffset;
			}
		}

		// Token: 0x1700135B RID: 4955
		// (get) Token: 0x06004EDC RID: 20188 RVA: 0x00144B95 File Offset: 0x00142D95
		private static int VersionOffset
		{
			get
			{
				if (Com2Properties.versionOffset == -1)
				{
					Com2Properties.versionOffset = Com2Properties.CountMemberOffset + 12;
				}
				return Com2Properties.versionOffset;
			}
		}

		// Token: 0x06004EDD RID: 20189 RVA: 0x00144BB4 File Offset: 0x00142DB4
		private unsafe long GetTypeInfoVersion(UnsafeNativeMethods.ITypeInfo pTypeInfo)
		{
			IntPtr zero = IntPtr.Zero;
			int typeAttr = pTypeInfo.GetTypeAttr(ref zero);
			if (!NativeMethods.Succeeded(typeAttr))
			{
				return 0L;
			}
			long result;
			try
			{
				System.Runtime.InteropServices.ComTypes.TYPEATTR typeattr;
				try
				{
					typeattr = *(System.Runtime.InteropServices.ComTypes.TYPEATTR*)((void*)zero);
				}
				catch
				{
					return 0L;
				}
				long num = 0L;
				int* ptr = (int*)(&num);
				byte* ptr2 = (byte*)(&typeattr);
				*ptr = *(int*)(ptr2 + Com2Properties.CountMemberOffset);
				ptr++;
				*ptr = *(int*)(ptr2 + Com2Properties.VersionOffset);
				result = num;
			}
			finally
			{
				pTypeInfo.ReleaseTypeAttr(zero);
			}
			return result;
		}

		// Token: 0x06004EDE RID: 20190 RVA: 0x00144C48 File Offset: 0x00142E48
		internal bool CheckValid(bool checkVersions, bool callDispose)
		{
			if (this.AlwaysValid)
			{
				return true;
			}
			bool flag = this.weakObjRef != null && this.weakObjRef.IsAlive;
			if (flag && checkVersions)
			{
				long[] array = this.GetTypeInfoVersions(this.weakObjRef.Target);
				if (array.Length != this.typeInfoVersions.Length)
				{
					flag = false;
				}
				else
				{
					for (int i = 0; i < array.Length; i++)
					{
						if (array[i] != this.typeInfoVersions[i])
						{
							flag = false;
							break;
						}
					}
				}
				if (!flag)
				{
					this.typeInfoVersions = array;
				}
			}
			if (!flag && callDispose)
			{
				this.Dispose();
			}
			return flag;
		}

		// Token: 0x06004EDF RID: 20191 RVA: 0x00144CD8 File Offset: 0x00142ED8
		internal void SetProps(Com2PropertyDescriptor[] props)
		{
			this.props = props;
			if (props != null)
			{
				for (int i = 0; i < props.Length; i++)
				{
					props[i].PropertyManager = this;
				}
			}
		}

		// Token: 0x0400341D RID: 13341
		private static TraceSwitch DbgCom2PropertiesSwitch = new TraceSwitch("DbgCom2Properties", "Com2Properties: debug Com2 properties manager");

		// Token: 0x0400341E RID: 13342
		private static long AGE_THRESHHOLD = (long)((ulong)-1294967296);

		// Token: 0x0400341F RID: 13343
		internal WeakReference weakObjRef;

		// Token: 0x04003420 RID: 13344
		private Com2PropertyDescriptor[] props;

		// Token: 0x04003421 RID: 13345
		private int defaultIndex = -1;

		// Token: 0x04003422 RID: 13346
		private long touchedTime;

		// Token: 0x04003423 RID: 13347
		private long[] typeInfoVersions;

		// Token: 0x04003424 RID: 13348
		private int alwaysValid;

		// Token: 0x04003425 RID: 13349
		private static Type[] extendedInterfaces = new Type[]
		{
			typeof(NativeMethods.ICategorizeProperties),
			typeof(NativeMethods.IProvidePropertyBuilder),
			typeof(NativeMethods.IPerPropertyBrowsing),
			typeof(NativeMethods.IVsPerPropertyBrowsing),
			typeof(NativeMethods.IManagedPerPropertyBrowsing)
		};

		// Token: 0x04003426 RID: 13350
		private static Type[] extendedInterfaceHandlerTypes = new Type[]
		{
			typeof(Com2ICategorizePropertiesHandler),
			typeof(Com2IProvidePropertyBuilderHandler),
			typeof(Com2IPerPropertyBrowsingHandler),
			typeof(Com2IVsPerPropertyBrowsingHandler),
			typeof(Com2IManagedPerPropertyBrowsingHandler)
		};

		// Token: 0x04003428 RID: 13352
		private static int countOffset = -1;

		// Token: 0x04003429 RID: 13353
		private static int versionOffset = -1;
	}
}
