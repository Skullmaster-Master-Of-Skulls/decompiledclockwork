using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x02000112 RID: 274
	internal class VarVec : IEnumerable<Var>, IEnumerable
	{
		// Token: 0x06000DAC RID: 3500 RVA: 0x0003D1B1 File Offset: 0x0003B3B1
		internal void Clear()
		{
			this.m_bitVector.Length = 0;
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x0003D1BF File Offset: 0x0003B3BF
		internal void And(VarVec other)
		{
			this.Align(other);
			this.m_bitVector.And(other.m_bitVector);
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x0003D1DA File Offset: 0x0003B3DA
		internal void Or(VarVec other)
		{
			this.Align(other);
			this.m_bitVector.Or(other.m_bitVector);
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x0003D1F8 File Offset: 0x0003B3F8
		internal void Minus(VarVec other)
		{
			VarVec varVec = this.m_command.CreateVarVec(other);
			varVec.m_bitVector.Length = this.m_bitVector.Length;
			varVec.m_bitVector.Not();
			this.And(varVec);
			this.m_command.ReleaseVarVec(varVec);
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x0003D248 File Offset: 0x0003B448
		internal bool Overlaps(VarVec other)
		{
			VarVec varVec = this.m_command.CreateVarVec(other);
			varVec.And(this);
			bool result = !varVec.IsEmpty;
			this.m_command.ReleaseVarVec(varVec);
			return result;
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x0003D280 File Offset: 0x0003B480
		internal bool Subsumes(VarVec other)
		{
			for (int i = 0; i < other.m_bitVector.Count; i++)
			{
				if (other.m_bitVector[i] && (i >= this.m_bitVector.Count || !this.m_bitVector[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x0003D2D0 File Offset: 0x0003B4D0
		internal void InitFrom(VarVec other)
		{
			this.Clear();
			this.m_bitVector.Length = other.m_bitVector.Length;
			this.m_bitVector.Or(other.m_bitVector);
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x0003D300 File Offset: 0x0003B500
		internal void InitFrom(IEnumerable<Var> other)
		{
			this.InitFrom(other, false);
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x0003D30C File Offset: 0x0003B50C
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

		// Token: 0x06000DB5 RID: 3509 RVA: 0x0003D368 File Offset: 0x0003B568
		public IEnumerator<Var> GetEnumerator()
		{
			return this.m_command.GetVarVecEnumerator(this);
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x0003D376 File Offset: 0x0003B576
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000DB7 RID: 3511 RVA: 0x0003D380 File Offset: 0x0003B580
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

		// Token: 0x06000DB8 RID: 3512 RVA: 0x0003D3C8 File Offset: 0x0003B5C8
		internal bool IsSet(Var v)
		{
			this.Align(v.Id);
			return this.m_bitVector.Get(v.Id);
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x0003D3E7 File Offset: 0x0003B5E7
		internal void Set(Var v)
		{
			this.Align(v.Id);
			this.m_bitVector.Set(v.Id, true);
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x0003D407 File Offset: 0x0003B607
		internal void Clear(Var v)
		{
			this.Align(v.Id);
			this.m_bitVector.Set(v.Id, false);
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000DBB RID: 3515 RVA: 0x0003D427 File Offset: 0x0003B627
		internal bool IsEmpty
		{
			get
			{
				return this.First == null;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000DBC RID: 3516 RVA: 0x0003D434 File Offset: 0x0003B634
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

		// Token: 0x06000DBD RID: 3517 RVA: 0x0003D47C File Offset: 0x0003B67C
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

		// Token: 0x06000DBE RID: 3518 RVA: 0x0003D4E0 File Offset: 0x0003B6E0
		internal VarVec(Command command)
		{
			this.m_bitVector = new BitArray(64);
			this.m_command = command;
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x0003D4FC File Offset: 0x0003B6FC
		private void Align(VarVec other)
		{
			if (other.m_bitVector.Count == this.m_bitVector.Count)
			{
				return;
			}
			if (other.m_bitVector.Count > this.m_bitVector.Count)
			{
				this.m_bitVector.Length = other.m_bitVector.Count;
				return;
			}
			other.m_bitVector.Length = this.m_bitVector.Count;
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x0003D567 File Offset: 0x0003B767
		private void Align(int idx)
		{
			if (idx >= this.m_bitVector.Count)
			{
				this.m_bitVector.Length = idx + 1;
			}
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x0003D588 File Offset: 0x0003B788
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

		// Token: 0x06000DC2 RID: 3522 RVA: 0x0003D60C File Offset: 0x0003B80C
		public VarVec Clone()
		{
			VarVec varVec = this.m_command.CreateVarVec();
			varVec.InitFrom(this);
			return varVec;
		}

		// Token: 0x040009D8 RID: 2520
		private BitArray m_bitVector;

		// Token: 0x040009D9 RID: 2521
		private Command m_command;

		// Token: 0x02000497 RID: 1175
		internal class VarVecEnumerator : IEnumerator<Var>, IDisposable, IEnumerator
		{
			// Token: 0x06003C08 RID: 15368 RVA: 0x000E24EB File Offset: 0x000E06EB
			internal VarVecEnumerator(VarVec vec)
			{
				this.Init(vec);
			}

			// Token: 0x06003C09 RID: 15369 RVA: 0x000E24FA File Offset: 0x000E06FA
			internal void Init(VarVec vec)
			{
				this.m_position = -1;
				this.m_command = vec.m_command;
				this.m_bitArray = vec.m_bitVector;
			}

			// Token: 0x17000AE1 RID: 2785
			// (get) Token: 0x06003C0A RID: 15370 RVA: 0x000E251B File Offset: 0x000E071B
			public Var Current
			{
				get
				{
					if (this.m_position < 0 || this.m_position >= this.m_bitArray.Count)
					{
						return null;
					}
					return this.m_command.GetVar(this.m_position);
				}
			}

			// Token: 0x17000AE2 RID: 2786
			// (get) Token: 0x06003C0B RID: 15371 RVA: 0x000E254C File Offset: 0x000E074C
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06003C0C RID: 15372 RVA: 0x000E2554 File Offset: 0x000E0754
			public bool MoveNext()
			{
				this.m_position++;
				while (this.m_position < this.m_bitArray.Count)
				{
					if (this.m_bitArray[this.m_position])
					{
						return true;
					}
					this.m_position++;
				}
				return false;
			}

			// Token: 0x06003C0D RID: 15373 RVA: 0x000E25A8 File Offset: 0x000E07A8
			public void Reset()
			{
				this.m_position = -1;
			}

			// Token: 0x06003C0E RID: 15374 RVA: 0x000E25B1 File Offset: 0x000E07B1
			public void Dispose()
			{
				GC.SuppressFinalize(this);
				this.m_bitArray = null;
				this.m_command.ReleaseVarVecEnumerator(this);
			}

			// Token: 0x04001A05 RID: 6661
			private int m_position;

			// Token: 0x04001A06 RID: 6662
			private Command m_command;

			// Token: 0x04001A07 RID: 6663
			private BitArray m_bitArray;
		}
	}
}
