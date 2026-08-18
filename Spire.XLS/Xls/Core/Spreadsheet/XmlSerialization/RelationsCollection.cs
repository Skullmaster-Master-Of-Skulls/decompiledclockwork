using System;
using System.Collections;
using System.Collections.Generic;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet.XmlSerialization
{
	// Token: 0x020005ED RID: 1517
	public class RelationsCollection : IEnumerable, ICloneable
	{
		// Token: 0x17000DF8 RID: 3576
		internal sprᦨ this[string A_0]
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				sprᦨ result;
				this.ᜂ.TryGetValue(A_0, out result);
				return result;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜂ[A_0] = value;
			}
		}

		// Token: 0x17000DF9 RID: 3577
		// (get) Token: 0x060059DD RID: 23005 RVA: 0x00385B28 File Offset: 0x00384B28
		public int Count
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return this.ᜂ.Count;
			}
		}

		// Token: 0x17000DFA RID: 3578
		// (get) Token: 0x060059DE RID: 23006 RVA: 0x00385B70 File Offset: 0x00384B70
		// (set) Token: 0x060059DF RID: 23007 RVA: 0x00385BB4 File Offset: 0x00384BB4
		public string ItemPath
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return this.ᜃ;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜃ = value;
			}
		}

		// Token: 0x060059E0 RID: 23008 RVA: 0x00385BF8 File Offset: 0x00384BF8
		public void Remove(string id)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.ᜂ.Remove(id);
		}

		// Token: 0x060059E1 RID: 23009 RVA: 0x00385C40 File Offset: 0x00384C40
		public void RemoveByContentType(string contentType)
		{
			int num = 3;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
				{
					if (false)
					{
					}
					if (true)
					{
					}
					Dictionary<string, sprᦨ>.Enumerator enumerator;
					switch (num)
					{
					case 0:
						goto IL_119;
					case 1:
						try
						{
							num = 6;
							for (;;)
							{
								switch (num)
								{
								case 0:
									goto IL_FE;
								case 1:
								{
									if (!enumerator.MoveNext())
									{
										num = 0;
										continue;
									}
									KeyValuePair<string, sprᦨ> keyValuePair = enumerator.Current;
									sprᦨ value = keyValuePair.Value;
									num = 3;
									continue;
								}
								case 2:
									goto IL_FE;
								case 3:
								{
									sprᦨ value;
									if (value.ᜃ() == contentType)
									{
										num = 4;
										continue;
									}
									break;
								}
								case 4:
								{
									KeyValuePair<string, sprᦨ> keyValuePair;
									this.ᜂ.Remove(keyValuePair.Key);
									num = 2;
									continue;
								}
								case 5:
									goto IL_109;
								}
								IL_C3:
								num = 1;
								continue;
								goto IL_C3;
								IL_FE:
								num = 5;
							}
							IL_109:
							return;
						}
						finally
						{
							((IDisposable)enumerator).Dispose();
						}
						goto IL_119;
					case 2:
						num = 4;
						continue;
					case 4:
						if (contentType.Length > 0)
						{
							goto IL_149;
						}
						return;
					}
					if (contentType != null)
					{
						num = 2;
						continue;
					}
					return;
					IL_119:
					enumerator = this.ᜂ.GetEnumerator();
					num = 1;
					continue;
				}
				}
				IL_149:
				num = 0;
			}
		}

		// Token: 0x060059E2 RID: 23010 RVA: 0x00385DB4 File Offset: 0x00384DB4
		internal sprᦨ ᜀ(string A_0, out string A_1)
		{
			switch (0)
			{
			default:
			{
				sprᦨ result;
				for (;;)
				{
					result = null;
					A_1 = null;
					int num = 0;
					for (;;)
					{
						Dictionary<string, sprᦨ>.Enumerator enumerator;
						switch (num)
						{
						case 0:
							if (A_0 != null)
							{
								num = 3;
								continue;
							}
							return result;
						case 1:
							goto IL_16D;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_16D;
							default:
								if (false)
								{
								}
								if (A_0.Length > 0)
								{
									num = 1;
									continue;
								}
								return result;
							}
							break;
						case 3:
							num = 2;
							continue;
						case 4:
							if (true)
							{
							}
							try
							{
								num = 1;
								for (;;)
								{
									switch (num)
									{
									case 0:
									{
										sprᦨ value;
										if (value.ᜃ() == A_0)
										{
											num = 6;
											continue;
										}
										break;
									}
									case 2:
									{
										if (!enumerator.MoveNext())
										{
											num = 5;
											continue;
										}
										KeyValuePair<string, sprᦨ> keyValuePair = enumerator.Current;
										sprᦨ value = keyValuePair.Value;
										num = 0;
										continue;
									}
									case 3:
										goto IL_F1;
									case 4:
										goto IL_E5;
									case 5:
										goto IL_E5;
									case 6:
									{
										sprᦨ value;
										result = value;
										KeyValuePair<string, sprᦨ> keyValuePair;
										A_1 = keyValuePair.Key;
										num = 4;
										continue;
									}
									}
									IL_AF:
									num = 2;
									continue;
									goto IL_AF;
									IL_E5:
									num = 3;
								}
								IL_F1:
								return result;
							}
							finally
							{
								((IDisposable)enumerator).Dispose();
							}
							goto IL_101;
						}
						break;
						IL_101:
						enumerator = this.ᜂ.GetEnumerator();
						num = 4;
						continue;
						IL_16D:
						goto IL_101;
					}
				}
				return result;
			}
			}
		}

		// Token: 0x060059E3 RID: 23011 RVA: 0x00385F44 File Offset: 0x00384F44
		public string FindRelationByTarget(string itemName)
		{
			switch (0)
			{
			default:
			{
				string result;
				for (;;)
				{
					result = null;
					if (true)
					{
					}
					int num = 2;
					for (;;)
					{
						Dictionary<string, sprᦨ>.Enumerator enumerator;
						switch (num)
						{
						case 0:
							num = 3;
							continue;
						case 1:
							goto IL_167;
						case 2:
							if (itemName != null)
							{
								num = 0;
								continue;
							}
							return result;
						case 3:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_167;
							default:
								if (false)
								{
								}
								if (itemName.Length > 0)
								{
									num = 1;
									continue;
								}
								return result;
							}
							break;
						case 4:
							try
							{
								num = 5;
								for (;;)
								{
									switch (num)
									{
									case 0:
										goto IL_E7;
									case 1:
									{
										if (!enumerator.MoveNext())
										{
											num = 0;
											continue;
										}
										KeyValuePair<string, sprᦨ> keyValuePair = enumerator.Current;
										sprᦨ value = keyValuePair.Value;
										num = 2;
										continue;
									}
									case 2:
									{
										sprᦨ value;
										if (value.ᜂ() == itemName)
										{
											num = 6;
											continue;
										}
										break;
									}
									case 3:
										goto IL_F3;
									case 4:
										goto IL_E7;
									case 6:
									{
										KeyValuePair<string, sprᦨ> keyValuePair;
										result = keyValuePair.Key;
										num = 4;
										continue;
									}
									}
									IL_B4:
									num = 1;
									continue;
									goto IL_B4;
									IL_E7:
									num = 3;
								}
								IL_F3:
								return result;
							}
							finally
							{
								((IDisposable)enumerator).Dispose();
							}
							goto IL_103;
						}
						break;
						IL_103:
						enumerator = this.ᜂ.GetEnumerator();
						num = 4;
						continue;
						IL_167:
						goto IL_103;
					}
				}
				return result;
			}
			}
		}

		// Token: 0x060059E4 RID: 23012 RVA: 0x003860CC File Offset: 0x003850CC
		public string GenerateRelationId()
		{
			int a_ = 8;
			string text;
			for (;;)
			{
				text = null;
				int num = 1;
				int num2 = 3;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						if (num >= 2147483647)
						{
							num2 = 1;
							continue;
						}
						text = RecordTableEnumerator.b("䰽ि♁", a_) + num;
						if (true)
						{
						}
						num2 = 5;
						continue;
					case 1:
						return text;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_35;
						default:
							if (false)
							{
							}
							num++;
							num2 = 4;
							continue;
						}
						break;
					case 3:
						goto IL_35;
					case 4:
						goto IL_37;
					case 5:
						if (this.ᜂ.ContainsKey(text))
						{
							num2 = 2;
							continue;
						}
						return text;
					}
					break;
					IL_37:
					num2 = 0;
					continue;
					IL_35:
					goto IL_37;
				}
			}
			return text;
		}

		// Token: 0x060059E5 RID: 23013 RVA: 0x003861A8 File Offset: 0x003851A8
		internal string ᜀ(sprᦨ A_0)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			string text = this.GenerateRelationId();
			this[text] = A_0;
			return text;
		}

		// Token: 0x060059E6 RID: 23014 RVA: 0x003861F4 File Offset: 0x003851F4
		public void Clear()
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.ᜂ.Clear();
		}

		// Token: 0x060059E7 RID: 23015 RVA: 0x0038623C File Offset: 0x0038523C
		internal RelationsCollection ᜀ()
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			RelationsCollection relationsCollection = (RelationsCollection)base.MemberwiseClone();
			relationsCollection.ᜂ = spr\u1CD3.ᜀ<string, sprᦨ>(this.ᜂ);
			return relationsCollection;
		}

		// Token: 0x060059E8 RID: 23016 RVA: 0x00386298 File Offset: 0x00385298
		object ICloneable.Clone()
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return this.ᜀ();
		}

		// Token: 0x060059E9 RID: 23017 RVA: 0x003862DC File Offset: 0x003852DC
		public IEnumerator GetEnumerator()
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return this.ᜂ.GetEnumerator();
		}

		// Token: 0x060059EA RID: 23018 RVA: 0x00386328 File Offset: 0x00385328
		// Note: this type is marked as 'beforefieldinit'.
		static RelationsCollection()
		{
			int a_ = 6;
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			RelationsCollection.ᜁ = RecordTableEnumerator.b("主眽␿", a_).Length;
		}

		// Token: 0x04002C0A RID: 11274
		private const string ᜀ = "rId";

		// Token: 0x04002C0B RID: 11275
		private byte[] \u25D8\u00A2\u0087\u008F;

		// Token: 0x04002C0C RID: 11276
		private static readonly int ᜁ;

		// Token: 0x04002C0D RID: 11277
		private Dictionary<string, sprᦨ> ᜂ = new Dictionary<string, sprᦨ>();

		// Token: 0x04002C0E RID: 11278
		private string[] \u2593\u0088\u00A4\u00A1;

		// Token: 0x04002C0F RID: 11279
		private float[] \u25D8\u0089\u0082\u0095;

		// Token: 0x04002C10 RID: 11280
		private string ᜃ;
	}
}
