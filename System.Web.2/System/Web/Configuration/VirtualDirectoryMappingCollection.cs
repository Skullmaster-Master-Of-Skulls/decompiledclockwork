using System;
using System.Collections;
using System.Collections.Specialized;
using System.Web.Util;

namespace System.Web.Configuration
{
	// Token: 0x02000770 RID: 1904
	[Serializable]
	public sealed class VirtualDirectoryMappingCollection : NameObjectCollectionBase
	{
		// Token: 0x06005BB1 RID: 23473 RVA: 0x0001634B File Offset: 0x0001454B
		public VirtualDirectoryMappingCollection() : base(StringComparer.OrdinalIgnoreCase)
		{
		}

		// Token: 0x17001AE2 RID: 6882
		// (get) Token: 0x06005BB2 RID: 23474 RVA: 0x00016A4D File Offset: 0x00014C4D
		public ICollection AllKeys
		{
			get
			{
				return base.BaseGetAllKeys();
			}
		}

		// Token: 0x17001AE3 RID: 6883
		public VirtualDirectoryMapping this[string virtualDirectory]
		{
			get
			{
				virtualDirectory = VirtualDirectoryMappingCollection.ValidateVirtualDirectoryParameter(virtualDirectory);
				return this.Get(virtualDirectory);
			}
		}

		// Token: 0x17001AE4 RID: 6884
		public VirtualDirectoryMapping this[int index]
		{
			get
			{
				return this.Get(index);
			}
		}

		// Token: 0x06005BB5 RID: 23477 RVA: 0x0013DADD File Offset: 0x0013BCDD
		public void Add(string virtualDirectory, VirtualDirectoryMapping mapping)
		{
			virtualDirectory = VirtualDirectoryMappingCollection.ValidateVirtualDirectoryParameter(virtualDirectory);
			if (mapping == null)
			{
				throw new ArgumentNullException("mapping");
			}
			if (this.Get(virtualDirectory) != null)
			{
				throw ExceptionUtil.ParameterInvalid("virtualDirectory");
			}
			mapping.SetVirtualDirectory(VirtualPath.CreateAbsoluteAllowNull(virtualDirectory));
			base.BaseAdd(virtualDirectory, mapping);
		}

		// Token: 0x06005BB6 RID: 23478 RVA: 0x0013DB1D File Offset: 0x0013BD1D
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x06005BB7 RID: 23479 RVA: 0x0013DB28 File Offset: 0x0013BD28
		public void CopyTo(VirtualDirectoryMapping[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			int count = this.Count;
			if (array.Length < count + index)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			int i = 0;
			int num = index;
			while (i < count)
			{
				array[num] = this.Get(i);
				i++;
				num++;
			}
		}

		// Token: 0x06005BB8 RID: 23480 RVA: 0x0013DB79 File Offset: 0x0013BD79
		public VirtualDirectoryMapping Get(int index)
		{
			return (VirtualDirectoryMapping)base.BaseGet(index);
		}

		// Token: 0x06005BB9 RID: 23481 RVA: 0x0013DB87 File Offset: 0x0013BD87
		public VirtualDirectoryMapping Get(string virtualDirectory)
		{
			virtualDirectory = VirtualDirectoryMappingCollection.ValidateVirtualDirectoryParameter(virtualDirectory);
			return (VirtualDirectoryMapping)base.BaseGet(virtualDirectory);
		}

		// Token: 0x06005BBA RID: 23482 RVA: 0x000166A9 File Offset: 0x000148A9
		public string GetKey(int index)
		{
			return base.BaseGetKey(index);
		}

		// Token: 0x06005BBB RID: 23483 RVA: 0x0013DB9D File Offset: 0x0013BD9D
		public void Remove(string virtualDirectory)
		{
			virtualDirectory = VirtualDirectoryMappingCollection.ValidateVirtualDirectoryParameter(virtualDirectory);
			base.BaseRemove(virtualDirectory);
		}

		// Token: 0x06005BBC RID: 23484 RVA: 0x0013DBAE File Offset: 0x0013BDAE
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		// Token: 0x06005BBD RID: 23485 RVA: 0x0013DBB8 File Offset: 0x0013BDB8
		internal VirtualDirectoryMappingCollection Clone()
		{
			VirtualDirectoryMappingCollection virtualDirectoryMappingCollection = new VirtualDirectoryMappingCollection();
			for (int i = 0; i < this.Count; i++)
			{
				VirtualDirectoryMapping virtualDirectoryMapping = this[i];
				virtualDirectoryMappingCollection.Add(virtualDirectoryMapping.VirtualDirectory, virtualDirectoryMapping.Clone());
			}
			return virtualDirectoryMappingCollection;
		}

		// Token: 0x06005BBE RID: 23486 RVA: 0x0013DBF8 File Offset: 0x0013BDF8
		private static string ValidateVirtualDirectoryParameter(string virtualDirectory)
		{
			VirtualPath virtualPath = VirtualPath.CreateAbsoluteAllowNull(virtualDirectory);
			return VirtualPath.GetVirtualPathString(virtualPath);
		}
	}
}
