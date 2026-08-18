using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;

namespace System.Security.Policy
{
	// Token: 0x020004A0 RID: 1184
	[ComVisible(true)]
	[Serializable]
	public sealed class Evidence : ICollection, IEnumerable
	{
		// Token: 0x06002EFB RID: 12027 RVA: 0x0009EF49 File Offset: 0x0009DF49
		public Evidence()
		{
			this.m_hostList = null;
			this.m_assemblyList = null;
			this.m_locked = false;
		}

		// Token: 0x06002EFC RID: 12028 RVA: 0x0009EF66 File Offset: 0x0009DF66
		public Evidence(Evidence evidence)
		{
			if (evidence == null)
			{
				return;
			}
			this.m_locked = false;
			this.Merge(evidence);
		}

		// Token: 0x06002EFD RID: 12029 RVA: 0x0009EF80 File Offset: 0x0009DF80
		public Evidence(object[] hostEvidence, object[] assemblyEvidence)
		{
			this.m_locked = false;
			if (hostEvidence != null)
			{
				this.m_hostList = ArrayList.Synchronized(new ArrayList(hostEvidence));
			}
			if (assemblyEvidence != null)
			{
				this.m_assemblyList = ArrayList.Synchronized(new ArrayList(assemblyEvidence));
			}
		}

		// Token: 0x06002EFE RID: 12030 RVA: 0x0009EFB8 File Offset: 0x0009DFB8
		internal Evidence(char[] buffer)
		{
			int i = 0;
			while (i < buffer.Length)
			{
				switch (buffer[i++])
				{
				case '\0':
				{
					IBuiltInEvidence builtInEvidence = new ApplicationDirectory();
					i = builtInEvidence.InitFromBuffer(buffer, i);
					this.AddAssembly(builtInEvidence);
					continue;
				}
				case '\u0001':
				{
					IBuiltInEvidence builtInEvidence2 = new Publisher();
					i = builtInEvidence2.InitFromBuffer(buffer, i);
					this.AddHost(builtInEvidence2);
					continue;
				}
				case '\u0002':
				{
					IBuiltInEvidence builtInEvidence3 = new StrongName();
					i = builtInEvidence3.InitFromBuffer(buffer, i);
					this.AddHost(builtInEvidence3);
					continue;
				}
				case '\u0003':
				{
					IBuiltInEvidence builtInEvidence4 = new Zone();
					i = builtInEvidence4.InitFromBuffer(buffer, i);
					this.AddHost(builtInEvidence4);
					continue;
				}
				case '\u0004':
				{
					IBuiltInEvidence builtInEvidence5 = new Url();
					i = builtInEvidence5.InitFromBuffer(buffer, i);
					this.AddHost(builtInEvidence5);
					continue;
				}
				case '\u0006':
				{
					IBuiltInEvidence builtInEvidence6 = new Site();
					i = builtInEvidence6.InitFromBuffer(buffer, i);
					this.AddHost(builtInEvidence6);
					continue;
				}
				case '\a':
				{
					IBuiltInEvidence builtInEvidence7 = new PermissionRequestEvidence();
					i = builtInEvidence7.InitFromBuffer(buffer, i);
					this.AddHost(builtInEvidence7);
					continue;
				}
				case '\b':
				{
					IBuiltInEvidence builtInEvidence8 = new Hash();
					i = builtInEvidence8.InitFromBuffer(buffer, i);
					this.AddHost(builtInEvidence8);
					continue;
				}
				case '\t':
				{
					IBuiltInEvidence builtInEvidence9 = new GacInstalled();
					i = builtInEvidence9.InitFromBuffer(buffer, i);
					this.AddHost(builtInEvidence9);
					continue;
				}
				}
				throw new SerializationException(Environment.GetResourceString("Serialization_UnableToFixup"));
			}
		}

		// Token: 0x06002EFF RID: 12031 RVA: 0x0009F11E File Offset: 0x0009E11E
		public void AddHost(object id)
		{
			if (this.m_hostList == null)
			{
				this.m_hostList = ArrayList.Synchronized(new ArrayList());
			}
			if (this.m_locked)
			{
				new SecurityPermission(SecurityPermissionFlag.ControlEvidence).Demand();
			}
			this.m_hostList.Add(id);
		}

		// Token: 0x06002F00 RID: 12032 RVA: 0x0009F159 File Offset: 0x0009E159
		public void AddAssembly(object id)
		{
			if (this.m_assemblyList == null)
			{
				this.m_assemblyList = ArrayList.Synchronized(new ArrayList());
			}
			this.m_assemblyList.Add(id);
		}

		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x06002F01 RID: 12033 RVA: 0x0009F180 File Offset: 0x0009E180
		// (set) Token: 0x06002F02 RID: 12034 RVA: 0x0009F188 File Offset: 0x0009E188
		public bool Locked
		{
			get
			{
				return this.m_locked;
			}
			set
			{
				if (!value)
				{
					new SecurityPermission(SecurityPermissionFlag.ControlEvidence).Demand();
					this.m_locked = false;
					return;
				}
				this.m_locked = true;
			}
		}

		// Token: 0x06002F03 RID: 12035 RVA: 0x0009F1A8 File Offset: 0x0009E1A8
		public void Merge(Evidence evidence)
		{
			if (evidence == null)
			{
				return;
			}
			if (evidence.m_hostList != null)
			{
				if (this.m_hostList == null)
				{
					this.m_hostList = ArrayList.Synchronized(new ArrayList());
				}
				if (evidence.m_hostList.Count != 0 && this.m_locked)
				{
					new SecurityPermission(SecurityPermissionFlag.ControlEvidence).Demand();
				}
				foreach (object value in evidence.m_hostList)
				{
					this.m_hostList.Add(value);
				}
			}
			if (evidence.m_assemblyList != null)
			{
				if (this.m_assemblyList == null)
				{
					this.m_assemblyList = ArrayList.Synchronized(new ArrayList());
				}
				foreach (object value2 in evidence.m_assemblyList)
				{
					this.m_assemblyList.Add(value2);
				}
			}
		}

		// Token: 0x06002F04 RID: 12036 RVA: 0x0009F26C File Offset: 0x0009E26C
		internal void MergeWithNoDuplicates(Evidence evidence)
		{
			if (evidence == null)
			{
				return;
			}
			IEnumerator enumerator;
			if (evidence.m_hostList != null)
			{
				if (this.m_hostList == null)
				{
					this.m_hostList = ArrayList.Synchronized(new ArrayList());
				}
				foreach (object obj in evidence.m_hostList)
				{
					Type type = obj.GetType();
					IEnumerator enumerator2 = this.m_hostList.GetEnumerator();
					while (enumerator2.MoveNext())
					{
						if (enumerator2.Current.GetType() == type)
						{
							this.m_hostList.Remove(enumerator2.Current);
							break;
						}
					}
					this.m_hostList.Add(enumerator.Current);
				}
			}
			if (evidence.m_assemblyList != null)
			{
				if (this.m_assemblyList == null)
				{
					this.m_assemblyList = ArrayList.Synchronized(new ArrayList());
				}
				foreach (object obj2 in evidence.m_assemblyList)
				{
					Type type2 = obj2.GetType();
					IEnumerator enumerator2 = this.m_assemblyList.GetEnumerator();
					while (enumerator2.MoveNext())
					{
						if (enumerator2.Current.GetType() == type2)
						{
							this.m_assemblyList.Remove(enumerator2.Current);
							break;
						}
					}
					this.m_assemblyList.Add(enumerator.Current);
				}
			}
		}

		// Token: 0x06002F05 RID: 12037 RVA: 0x0009F39C File Offset: 0x0009E39C
		public void CopyTo(Array array, int index)
		{
			int num = index;
			if (this.m_hostList != null)
			{
				this.m_hostList.CopyTo(array, num);
				num += this.m_hostList.Count;
			}
			if (this.m_assemblyList != null)
			{
				this.m_assemblyList.CopyTo(array, num);
			}
		}

		// Token: 0x06002F06 RID: 12038 RVA: 0x0009F3E3 File Offset: 0x0009E3E3
		public IEnumerator GetHostEnumerator()
		{
			if (this.m_hostList == null)
			{
				this.m_hostList = ArrayList.Synchronized(new ArrayList());
			}
			return this.m_hostList.GetEnumerator();
		}

		// Token: 0x06002F07 RID: 12039 RVA: 0x0009F408 File Offset: 0x0009E408
		public IEnumerator GetAssemblyEnumerator()
		{
			if (this.m_assemblyList == null)
			{
				this.m_assemblyList = ArrayList.Synchronized(new ArrayList());
			}
			return this.m_assemblyList.GetEnumerator();
		}

		// Token: 0x06002F08 RID: 12040 RVA: 0x0009F42D File Offset: 0x0009E42D
		public IEnumerator GetEnumerator()
		{
			return new EvidenceEnumerator(this);
		}

		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x06002F09 RID: 12041 RVA: 0x0009F435 File Offset: 0x0009E435
		public int Count
		{
			get
			{
				return ((this.m_hostList != null) ? this.m_hostList.Count : 0) + ((this.m_assemblyList != null) ? this.m_assemblyList.Count : 0);
			}
		}

		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x06002F0A RID: 12042 RVA: 0x0009F464 File Offset: 0x0009E464
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x06002F0B RID: 12043 RVA: 0x0009F467 File Offset: 0x0009E467
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x06002F0C RID: 12044 RVA: 0x0009F46A File Offset: 0x0009E46A
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002F0D RID: 12045 RVA: 0x0009F470 File Offset: 0x0009E470
		internal Evidence Copy()
		{
			char[] array = PolicyManager.MakeEvidenceArray(this, true);
			if (array != null)
			{
				return new Evidence(array);
			}
			new PermissionSet(true).Assert();
			MemoryStream memoryStream = new MemoryStream();
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			binaryFormatter.Serialize(memoryStream, this);
			memoryStream.Position = 0L;
			return (Evidence)binaryFormatter.Deserialize(memoryStream);
		}

		// Token: 0x06002F0E RID: 12046 RVA: 0x0009F4C4 File Offset: 0x0009E4C4
		internal Evidence ShallowCopy()
		{
			Evidence evidence = new Evidence();
			IEnumerator enumerator = this.GetHostEnumerator();
			while (enumerator.MoveNext())
			{
				object id = enumerator.Current;
				evidence.AddHost(id);
			}
			enumerator = this.GetAssemblyEnumerator();
			while (enumerator.MoveNext())
			{
				object id2 = enumerator.Current;
				evidence.AddAssembly(id2);
			}
			return evidence;
		}

		// Token: 0x06002F0F RID: 12047 RVA: 0x0009F512 File Offset: 0x0009E512
		[ComVisible(false)]
		public void Clear()
		{
			this.m_hostList = null;
			this.m_assemblyList = null;
		}

		// Token: 0x06002F10 RID: 12048 RVA: 0x0009F524 File Offset: 0x0009E524
		[ComVisible(false)]
		public void RemoveType(Type t)
		{
			for (int i = 0; i < ((this.m_hostList == null) ? 0 : this.m_hostList.Count); i++)
			{
				if (this.m_hostList[i].GetType() == t)
				{
					this.m_hostList.RemoveAt(i--);
				}
			}
			for (int i = 0; i < ((this.m_assemblyList == null) ? 0 : this.m_assemblyList.Count); i++)
			{
				if (this.m_assemblyList[i].GetType() == t)
				{
					this.m_assemblyList.RemoveAt(i--);
				}
			}
		}

		// Token: 0x06002F11 RID: 12049 RVA: 0x0009F5BC File Offset: 0x0009E5BC
		[ComVisible(false)]
		public override bool Equals(object obj)
		{
			Evidence evidence = obj as Evidence;
			if (evidence == null)
			{
				return false;
			}
			if (this.m_hostList != null && evidence.m_hostList != null)
			{
				if (this.m_hostList.Count != evidence.m_hostList.Count)
				{
					return false;
				}
				int count = this.m_hostList.Count;
				for (int i = 0; i < count; i++)
				{
					bool flag = false;
					for (int j = 0; j < count; j++)
					{
						if (object.Equals(this.m_hostList[i], evidence.m_hostList[j]))
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						return false;
					}
				}
			}
			else if (this.m_hostList != null || evidence.m_hostList != null)
			{
				return false;
			}
			if (this.m_assemblyList != null && evidence.m_assemblyList != null)
			{
				if (this.m_assemblyList.Count != evidence.m_assemblyList.Count)
				{
					return false;
				}
				int count2 = this.m_assemblyList.Count;
				for (int k = 0; k < count2; k++)
				{
					bool flag2 = false;
					for (int l = 0; l < count2; l++)
					{
						if (object.Equals(this.m_assemblyList[k], evidence.m_assemblyList[l]))
						{
							flag2 = true;
							break;
						}
					}
					if (!flag2)
					{
						return false;
					}
				}
			}
			else if (this.m_assemblyList != null || evidence.m_assemblyList != null)
			{
				return false;
			}
			return true;
		}

		// Token: 0x06002F12 RID: 12050 RVA: 0x0009F708 File Offset: 0x0009E708
		[ComVisible(false)]
		public override int GetHashCode()
		{
			int num = 0;
			if (this.m_hostList != null)
			{
				int count = this.m_hostList.Count;
				for (int i = 0; i < count; i++)
				{
					num ^= this.m_hostList[i].GetHashCode();
				}
			}
			if (this.m_assemblyList != null)
			{
				int count2 = this.m_assemblyList.Count;
				for (int j = 0; j < count2; j++)
				{
					num ^= this.m_assemblyList[j].GetHashCode();
				}
			}
			return num;
		}

		// Token: 0x06002F13 RID: 12051 RVA: 0x0009F788 File Offset: 0x0009E788
		internal object FindType(Type t)
		{
			for (int i = 0; i < ((this.m_hostList == null) ? 0 : this.m_hostList.Count); i++)
			{
				if (this.m_hostList[i].GetType() == t)
				{
					return this.m_hostList[i];
				}
			}
			for (int i = 0; i < ((this.m_assemblyList == null) ? 0 : this.m_assemblyList.Count); i++)
			{
				if (this.m_assemblyList[i].GetType() == t)
				{
					return this.m_hostList[i];
				}
			}
			return null;
		}

		// Token: 0x06002F14 RID: 12052 RVA: 0x0009F81C File Offset: 0x0009E81C
		internal void MarkAllEvidenceAsUsed()
		{
			foreach (object obj in this)
			{
				IDelayEvaluatedEvidence delayEvaluatedEvidence = obj as IDelayEvaluatedEvidence;
				if (delayEvaluatedEvidence != null)
				{
					delayEvaluatedEvidence.MarkUsed();
				}
			}
		}

		// Token: 0x06002F15 RID: 12053 RVA: 0x0009F874 File Offset: 0x0009E874
		private bool WasStrongNameEvidenceUsed()
		{
			IDelayEvaluatedEvidence delayEvaluatedEvidence = this.FindType(typeof(StrongName)) as IDelayEvaluatedEvidence;
			return delayEvaluatedEvidence != null && delayEvaluatedEvidence.WasUsed;
		}

		// Token: 0x040017F5 RID: 6133
		private IList m_hostList;

		// Token: 0x040017F6 RID: 6134
		private IList m_assemblyList;

		// Token: 0x040017F7 RID: 6135
		private bool m_locked;
	}
}
