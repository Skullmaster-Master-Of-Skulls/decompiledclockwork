using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x0200064A RID: 1610
	internal class VarVec : IEnumerable<Var>, IEnumerable
	{
		// Token: 0x06003EFF RID: 16127 RVA: 0x00120620 File Offset: 0x0011E820
		internal void Clear()
		{
			this.m_bitVector.Length = 0;
		}

		// Token: 0x06003F00 RID: 16128 RVA: 0x0012062E File Offset: 0x0011E82E
		internal void And(VarVec other)
		{
			this.Align(other);
			this.m_bitVector.And(other.m_bitVector);
		}

		// Token: 0x06003F01 RID: 16129 RVA: 0x00120649 File Offset: 0x0011E849
		internal void Or(VarVec other)
		{
			this.Align(other);
			this.m_bitVector.Or(other.m_bitVector);
		}

		// Token: 0x06003F02 RID: 16130 RVA: 0x00120664 File Offset: 0x0011E864
		internal void Minus(VarVec other)
		{
			VarVec varVec = this.m_command.CreateVarVec(other);
			varVec.m_bitVector.Length = this.m_bitVector.Length;
			varVec.m_bitVector.Not();
			this.And(varVec);
			this.m_command.ReleaseVarVec(varVec);
		}

		// Token: 0x06003F03 RID: 16131 RVA: 0x001206B4 File Offset: 0x0011E8B4
		internal bool Overlaps(VarVec other)
		{
			VarVec varVec = this.m_command.CreateVarVec(other);
			varVec.And(this);
			bool result = !varVec.IsEmpty;
			this.m_command.ReleaseVarVec(varVec);
			return result;
		}

		// Token: 0x06003F04 RID: 16132 RVA: 0x001206EC File Offset: 0x0011E8EC
		internal bool Subsumes(VarVec other)
		{
			for (int i = 0; i < other.m_bitVector.Length; i++)
			{
				if (other.m_bitVector[i] && (i >= this.m_bitVector.Length || !this.m_bitVector[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003F05 RID: 16133 RVA: 0x0012073C File Offset: 0x0011E93C
		internal void InitFrom(VarVec other)
		{
			this.Clear();
			this.m_bitVector.Length = other.m_bitVector.Length;
			this.m_bitVector.Or(other.m_bitVector);
		}

		// Token: 0x06003F06 RID: 16134 RVA: 0x0012076C File Offset: 0x0011E96C
		internal void InitFrom(IEnumerable<Var> other)
		{
			this.InitFrom(other, false);
		}

		// Token: 0x06003F07 RID: 16135 RVA: 0x00120778 File Offset: 0x0011E978
		internal void InitFrom(IEnumerable<Var> other, bool ignoreParameters)
		{
			this.Clear();
			foreach (Var var in other)
			{
				if (!ignoreParameters || var.VarType != VarType.Parameter)
				{
					this.Set(var);
				}
			}
		}

		// Token: 0x06003F08 RID: 16136 RVA: 0x001207D4 File Offset: 0x0011E9D4
		public IEnumerator<Var> GetEnumerator()
		{
			return this.m_command.GetVarVecEnumerator(this);
		}

		// Token: 0x06003F09 RID: 16137 RVA: 0x001207E2 File Offset: 0x0011E9E2
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x06003F0A RID: 16138 RVA: 0x001207EC File Offset: 0x0011E9EC
		internal int Count
		{
			get
			{
				int num = 0;
				foreach (Var var in this)
				{
					num++;
				}
				return num;
			}
		}

		// Token: 0x06003F0B RID: 16139 RVA: 0x00120834 File Offset: 0x0011EA34
		internal bool IsSet(Var v)
		{
			this.Align(v.Id);
			return this.m_bitVector.Get(v.Id);
		}

		// Token: 0x06003F0C RID: 16140 RVA: 0x00120853 File Offset: 0x0011EA53
		internal void Set(Var v)
		{
			this.Align(v.Id);
			this.m_bitVector.Set(v.Id, true);
		}

		// Token: 0x06003F0D RID: 16141 RVA: 0x00120873 File Offset: 0x0011EA73
		internal void Clear(Var v)
		{
			this.Align(v.Id);
			this.m_bitVector.Set(v.Id, false);
		}

		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x06003F0E RID: 16142 RVA: 0x00120893 File Offset: 0x0011EA93
		internal bool IsEmpty
		{
			get
			{
				return this.First == null;
			}
		}

		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x06003F0F RID: 16143 RVA: 0x001208A0 File Offset: 0x0011EAA0
		internal Var First
		{
			get
			{
				using (IEnumerator<Var> enumerator = this.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						return enumerator.Current;
					}
				}
				return null;
			}
		}

		// Token: 0x06003F10 RID: 16144 RVA: 0x001208E8 File Offset: 0x0011EAE8
		internal VarVec Remap(Dictionary<Var, Var> varMap)
		{
			VarVec varVec = this.m_command.CreateVarVec();
			foreach (Var var in this)
			{
				Var v;
				if (!varMap.TryGetValue(var, out v))
				{
					v = var;
				}
				varVec.Set(v);
			}
			return varVec;
		}

		// Token: 0x06003F11 RID: 16145 RVA: 0x0012094C File Offset: 0x0011EB4C
		internal VarVec(Command command)
		{
			this.m_bitVector = new BitArray(64);
			this.m_command = command;
		}

		// Token: 0x06003F12 RID: 16146 RVA: 0x00120968 File Offset: 0x0011EB68
		private void Align(VarVec other)
		{
			if (other.m_bitVector.Length == this.m_bitVector.Length)
			{
				return;
			}
			if (other.m_bitVector.Length > this.m_bitVector.Length)
			{
				this.m_bitVector.Length = other.m_bitVector.Length;
				return;
			}
			other.m_bitVector.Length = this.m_bitVector.Length;
		}

		// Token: 0x06003F13 RID: 16147 RVA: 0x001209D3 File Offset: 0x0011EBD3
		private void Align(int idx)
		{
			if (idx >= this.m_bitVector.Length)
			{
				this.m_bitVector.Length = idx + 1;
			}
		}

		// Token: 0x06003F14 RID: 16148 RVA: 0x001209F4 File Offset: 0x0011EBF4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text = string.Empty;
			foreach (Var var in this)
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0}{1}", new object[]
				{
					text,
					var.Id
				});
				text = ",";
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003F15 RID: 16149 RVA: 0x00120A7C File Offset: 0x0011EC7C
		public VarVec Clone()
		{
			VarVec varVec = this.m_command.CreateVarVec();
			varVec.InitFrom(this);
			return varVec;
		}

		// Token: 0x0400178E RID: 6030
		private readonly BitArray m_bitVector;

		// Token: 0x0400178F RID: 6031
		private readonly Command m_command;

		// Token: 0x0200064B RID: 1611
		internal class VarVecEnumerator : IEnumerator<Var>, IEnumerator, IDisposable
		{
			// Token: 0x06003F16 RID: 16150 RVA: 0x00120A9D File Offset: 0x0011EC9D
			internal VarVecEnumerator(VarVec vec)
			{
				this.Init(vec);
			}

			// Token: 0x06003F17 RID: 16151 RVA: 0x00120AAC File Offset: 0x0011ECAC
			internal void Init(VarVec vec)
			{
				this.m_position = -1;
				this.m_command = vec.m_command;
				this.m_bitArray = vec.m_bitVector;
			}

			// Token: 0x170009BE RID: 2494
			// (get) Token: 0x06003F18 RID: 16152 RVA: 0x00120ACD File Offset: 0x0011ECCD
			public Var Current
			{
				get
				{
					if (this.m_position < 0 || this.m_position >= this.m_bitArray.Length)
					{
						return null;
					}
					return this.m_command.GetVar(this.m_position);
				}
			}

			// Token: 0x170009BF RID: 2495
			// (get) Token: 0x06003F19 RID: 16153 RVA: 0x00120AFE File Offset: 0x0011ECFE
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06003F1A RID: 16154 RVA: 0x00120B08 File Offset: 0x0011ED08
			public bool MoveNext()
			{
				this.m_position++;
				while (this.m_position < this.m_bitArray.Length)
				{
					if (this.m_bitArray[this.m_position])
					{
						return true;
					}
					this.m_position++;
				}
				return false;
			}

			// Token: 0x06003F1B RID: 16155 RVA: 0x00120B5C File Offset: 0x0011ED5C
			public void Reset()
			{
				this.m_position = -1;
			}

			// Token: 0x06003F1C RID: 16156 RVA: 0x00120B65 File Offset: 0x0011ED65
			public void Dispose()
			{
				GC.SuppressFinalize(this);
				this.m_bitArray = null;
				this.m_command.ReleaseVarVecEnumerator(this);
			}

			// Token: 0x04001790 RID: 6032
			private int m_position;

			// Token: 0x04001791 RID: 6033
			private Command m_command;

			// Token: 0x04001792 RID: 6034
			private BitArray m_bitArray;
		}
	}
}
