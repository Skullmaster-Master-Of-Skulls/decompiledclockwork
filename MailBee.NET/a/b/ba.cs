using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;
using a.h;
using MailBee.Mime;
using MailBee.Tnef;

namespace a.b
{
	// Token: 0x02000251 RID: 593
	internal class ba
	{
		// Token: 0x0600143B RID: 5179 RVA: 0x0005CCA7 File Offset: 0x0005BCA7
		public string r()
		{
			return this.j;
		}

		// Token: 0x0600143C RID: 5180 RVA: 0x0005CCAF File Offset: 0x0005BCAF
		public void c(string A_0)
		{
			this.j = A_0;
		}

		// Token: 0x0600143D RID: 5181 RVA: 0x0005CCB8 File Offset: 0x0005BCB8
		public string i()
		{
			return this.e;
		}

		// Token: 0x0600143E RID: 5182 RVA: 0x0005CCC0 File Offset: 0x0005BCC0
		public void h(string A_0)
		{
			this.e = A_0;
		}

		// Token: 0x0600143F RID: 5183 RVA: 0x0005CCC9 File Offset: 0x0005BCC9
		public string h()
		{
			return this.f;
		}

		// Token: 0x06001440 RID: 5184 RVA: 0x0005CCD1 File Offset: 0x0005BCD1
		public void k(string A_0)
		{
			this.f = A_0;
		}

		// Token: 0x06001441 RID: 5185 RVA: 0x0005CCDA File Offset: 0x0005BCDA
		public bool t()
		{
			return this.g;
		}

		// Token: 0x06001442 RID: 5186 RVA: 0x0005CCE2 File Offset: 0x0005BCE2
		public void a(bool A_0)
		{
			this.g = A_0;
		}

		// Token: 0x06001443 RID: 5187 RVA: 0x0005CCEB File Offset: 0x0005BCEB
		public string e()
		{
			return this.c;
		}

		// Token: 0x06001444 RID: 5188 RVA: 0x0005CCF3 File Offset: 0x0005BCF3
		public void f(string A_0)
		{
			this.c = A_0;
		}

		// Token: 0x06001445 RID: 5189 RVA: 0x0005CCFC File Offset: 0x0005BCFC
		public string d()
		{
			return this.d;
		}

		// Token: 0x06001446 RID: 5190 RVA: 0x0005CD04 File Offset: 0x0005BD04
		public void j(string A_0)
		{
			this.d = A_0;
		}

		// Token: 0x06001447 RID: 5191 RVA: 0x0005CD0D File Offset: 0x0005BD0D
		public string n()
		{
			return this.i;
		}

		// Token: 0x06001448 RID: 5192 RVA: 0x0005CD15 File Offset: 0x0005BD15
		public void g(string A_0)
		{
			this.i = A_0;
		}

		// Token: 0x06001449 RID: 5193 RVA: 0x0005CD1E File Offset: 0x0005BD1E
		public Encoding f()
		{
			return this.n;
		}

		// Token: 0x0600144A RID: 5194 RVA: 0x0005CD28 File Offset: 0x0005BD28
		public ArrayList l()
		{
			if (this.ab.Count > 0)
			{
				return this.ab;
			}
			if (this.x == null || this.x.Length == 0)
			{
				return null;
			}
			foreach (string text in this.x)
			{
				foreach (object obj in this.aa)
				{
					string[] array2 = (string[])obj;
					if (text == array2[0])
					{
						this.ab.Add(new string[]
						{
							array2[0],
							array2[1]
						});
					}
				}
			}
			return this.ab;
		}

		// Token: 0x0600144B RID: 5195 RVA: 0x0005CDF4 File Offset: 0x0005BDF4
		public void b(ArrayList A_0)
		{
			this.ab = A_0;
		}

		// Token: 0x0600144C RID: 5196 RVA: 0x0005CE00 File Offset: 0x0005BE00
		public ArrayList p()
		{
			if (this.ac.Count > 0)
			{
				return this.ac;
			}
			if (this.y == null || this.y.Length == 0)
			{
				return null;
			}
			foreach (string text in this.y)
			{
				foreach (object obj in this.aa)
				{
					string[] array2 = (string[])obj;
					if (text == array2[0])
					{
						this.ac.Add(new string[]
						{
							array2[0],
							array2[1]
						});
					}
				}
			}
			return this.ac;
		}

		// Token: 0x0600144D RID: 5197 RVA: 0x0005CECC File Offset: 0x0005BECC
		public void a(ArrayList A_0)
		{
			this.ac = A_0;
		}

		// Token: 0x0600144E RID: 5198 RVA: 0x0005CED8 File Offset: 0x0005BED8
		public ArrayList g()
		{
			if (this.ad.Count > 0)
			{
				return this.ad;
			}
			if (this.z == null || this.z.Length == 0)
			{
				return null;
			}
			foreach (string text in this.z)
			{
				foreach (object obj in this.aa)
				{
					string[] array2 = (string[])obj;
					if (text == array2[0])
					{
						this.ad.Add(new string[]
						{
							array2[0],
							array2[1]
						});
					}
				}
			}
			return this.ad;
		}

		// Token: 0x0600144F RID: 5199 RVA: 0x0005CFA4 File Offset: 0x0005BFA4
		public void d(ArrayList A_0)
		{
			this.ad = A_0;
		}

		// Token: 0x06001450 RID: 5200 RVA: 0x0005CFAD File Offset: 0x0005BFAD
		public string k()
		{
			return this.k;
		}

		// Token: 0x06001451 RID: 5201 RVA: 0x0005CFB5 File Offset: 0x0005BFB5
		public void d(string A_0)
		{
			this.k = A_0;
		}

		// Token: 0x06001452 RID: 5202 RVA: 0x0005CFBE File Offset: 0x0005BFBE
		public byte[] c()
		{
			return this.l;
		}

		// Token: 0x06001453 RID: 5203 RVA: 0x0005CFC6 File Offset: 0x0005BFC6
		public void a(byte[] A_0)
		{
			this.l = A_0;
		}

		// Token: 0x06001454 RID: 5204 RVA: 0x0005CFCF File Offset: 0x0005BFCF
		public string a()
		{
			return this.m;
		}

		// Token: 0x06001455 RID: 5205 RVA: 0x0005CFD7 File Offset: 0x0005BFD7
		public void i(string A_0)
		{
			this.m = A_0;
		}

		// Token: 0x06001456 RID: 5206 RVA: 0x0005CFE0 File Offset: 0x0005BFE0
		public a7 u()
		{
			return this.o;
		}

		// Token: 0x06001457 RID: 5207 RVA: 0x0005CFE8 File Offset: 0x0005BFE8
		public DateTime q()
		{
			return this.p;
		}

		// Token: 0x06001458 RID: 5208 RVA: 0x0005CFF0 File Offset: 0x0005BFF0
		public void c(DateTime A_0)
		{
			this.p = A_0;
		}

		// Token: 0x06001459 RID: 5209 RVA: 0x0005CFF9 File Offset: 0x0005BFF9
		public DateTime m()
		{
			return this.q;
		}

		// Token: 0x0600145A RID: 5210 RVA: 0x0005D001 File Offset: 0x0005C001
		public void b(DateTime A_0)
		{
			this.q = A_0;
		}

		// Token: 0x0600145B RID: 5211 RVA: 0x0005D00A File Offset: 0x0005C00A
		public MailPriority j()
		{
			return this.s;
		}

		// Token: 0x0600145C RID: 5212 RVA: 0x0005D012 File Offset: 0x0005C012
		public void a(MailPriority A_0)
		{
			this.s = A_0;
		}

		// Token: 0x0600145D RID: 5213 RVA: 0x0005D01B File Offset: 0x0005C01B
		public DateTime b()
		{
			return this.r;
		}

		// Token: 0x0600145E RID: 5214 RVA: 0x0005D023 File Offset: 0x0005C023
		public void a(DateTime A_0)
		{
			this.r = A_0;
		}

		// Token: 0x0600145F RID: 5215 RVA: 0x0005D02C File Offset: 0x0005C02C
		public string s()
		{
			return this.t;
		}

		// Token: 0x06001460 RID: 5216 RVA: 0x0005D034 File Offset: 0x0005C034
		public void l(string A_0)
		{
			this.t = A_0;
		}

		// Token: 0x06001461 RID: 5217 RVA: 0x0005D03D File Offset: 0x0005C03D
		public string o()
		{
			return this.u;
		}

		// Token: 0x06001462 RID: 5218 RVA: 0x0005D045 File Offset: 0x0005C045
		public void e(string A_0)
		{
			this.u = A_0;
		}

		// Token: 0x06001463 RID: 5219 RVA: 0x0005D04E File Offset: 0x0005C04E
		public void a(ho A_0)
		{
			this.v.Add(A_0);
		}

		// Token: 0x06001464 RID: 5220 RVA: 0x0005D060 File Offset: 0x0005C060
		public void a(string A_0, object A_1, bool A_2, Encoding A_3, bool A_4)
		{
			if (A_0 == null || A_1 == null)
			{
				return;
			}
			A_0 = string.Intern(A_0);
			uint num = global::b.a(A_0);
			if (num <= 753434108U)
			{
				if (num <= 462929855U)
				{
					if (num <= 79693691U)
					{
						if (num != 62916072U)
						{
							if (num != 79693691U)
							{
								goto IL_D1E;
							}
							if (!(A_0 == "0e03"))
							{
								goto IL_D1E;
							}
							this.y = b8.a((string)A_1).Split(new char[]
							{
								';'
							});
							for (int i = 0; i < this.y.Length; i++)
							{
								this.y[i] = this.y[i].TrimStart(new char[]
								{
									' '
								}).Trim(new char[]
								{
									'"'
								});
							}
							return;
						}
						else
						{
							if (!(A_0 == "0e02"))
							{
								goto IL_D1E;
							}
							this.z = b8.a((string)A_1).Split(new char[]
							{
								';'
							});
							for (int j = 0; j < this.z.Length; j++)
							{
								this.z[j] = this.z[j].TrimStart(new char[]
								{
									' '
								}).Trim(new char[]
								{
									'"'
								});
							}
							return;
						}
					}
					else if (num != 163581786U)
					{
						if (num != 299789532U)
						{
							if (num != 462929855U)
							{
								goto IL_D1E;
							}
							if (!(A_0 == "1009"))
							{
								goto IL_D1E;
							}
							if (!(A_1 is byte[]) || ((byte[])A_1).Length == 0)
							{
								return;
							}
							try
							{
								byte[] array = global::a.h.f.a((byte[])A_1);
								Encoding encoding = (this.f() == null) ? A_3 : this.f();
								if (array.Length > 100)
								{
									string @string = Encoding.Default.GetString(array, 0, 100);
									int num2 = @string.IndexOf("\\ansicpg");
									if (num2 > -1)
									{
										int num3 = @string.IndexOf('\\', num2 + "\\ansicpg".Length);
										if (num3 > -1)
										{
											string text = @string.Substring(num2 + "\\ansicpg".Length, num3 - (num2 + "\\ansicpg".Length));
											try
											{
												int num4 = int.Parse(text);
												if (encoding.CodePage != num4)
												{
													encoding = Encoding.GetEncoding(num4);
												}
											}
											catch (Exception)
											{
											}
										}
									}
								}
								this.d(encoding.GetString(array, 0, array.Length));
								this.l = array;
								if ((this.a() == null || this.a() == string.Empty || A_2) && ba.a(this.k()))
								{
									this.i(ba.a(this.k(), encoding));
								}
								return;
							}
							catch (MailBeeTnefParsingException)
							{
								return;
							}
						}
						else
						{
							if (!(A_0 == "properties"))
							{
								goto IL_D1E;
							}
							if (A_1 is byte[])
							{
								int num5 = A_4 ? 24 : 32;
								int num6 = (((byte[])A_1).Length - num5) / 16;
								byte[][] array2 = new byte[num6][];
								for (int k = 0; k < num6; k++)
								{
									array2[k] = new byte[16];
									Buffer.BlockCopy((byte[])A_1, k * 16 + num5, array2[k], 0, 16);
								}
								for (int l = 0; l < num6; l++)
								{
									int num7 = (int)BitConverter.ToInt16(array2[l], 0);
									int num8 = (int)BitConverter.ToInt16(array2[l], 2);
									if (num7 == 64)
									{
										long fileTime = BitConverter.ToInt64(array2[l], 8);
										if (num8 != 57)
										{
											if (num8 != 3590)
											{
												if (num8 == 12296)
												{
													this.c(DateTime.FromFileTime(fileTime));
												}
											}
											else
											{
												this.b(DateTime.FromFileTime(fileTime));
											}
										}
										else
										{
											this.a(DateTime.FromFileTime(fileTime));
										}
									}
									if (num7 == 3 && num8 == 38)
									{
										byte b = array2[l][8];
										if (b != 1)
										{
											if (b == 255)
											{
												this.a(MailPriority.Low);
											}
										}
										else
										{
											this.a(MailPriority.High);
										}
									}
									if (num7 == 3 && num8 == 16350)
									{
										int codepage = BitConverter.ToInt32(array2[l], 8);
										try
										{
											this.n = Encoding.GetEncoding(codepage);
										}
										catch (ArgumentException)
										{
										}
									}
								}
								return;
							}
							return;
						}
					}
					else
					{
						if (!(A_0 == "0e04"))
						{
							goto IL_D1E;
						}
						this.x = b8.a((string)A_1).Split(new char[]
						{
							';'
						});
						for (int m = 0; m < this.x.Length; m++)
						{
							this.x[m] = this.x[m].TrimStart(new char[]
							{
								' '
							}).Trim(new char[]
							{
								'"'
							});
						}
						return;
					}
				}
				else if (num <= 580373188U)
				{
					if (num != 500720787U)
					{
						if (num != 534276025U)
						{
							if (num != 580373188U)
							{
								goto IL_D1E;
							}
							if (!(A_0 == "1000"))
							{
								goto IL_D1E;
							}
							if (A_1 is string)
							{
								ArrayList arrayList = new ArrayList(((string)A_1).Split(new string[]
								{
									"\r\n"
								}, StringSplitOptions.None));
								for (int n = arrayList.Count - 1; n > 0; n--)
								{
									if (((string)arrayList[n - 1]).Length == 72 && ((string)arrayList[n]).Length > 0 && ((string)arrayList[n - 1])[71] != ' ' && ((string)arrayList[n - 1])[71] != '\t' && ((string)arrayList[n])[0] != ' ' && ((string)arrayList[n])[0] != '\t')
									{
										if (((string)arrayList[n - 1]).IndexOf(' ') == -1 && ((string)arrayList[n - 1]).IndexOf('\t') == -1)
										{
											ArrayList arrayList2 = arrayList;
											int index = n - 1;
											arrayList2[index] += (string)arrayList[n];
										}
										else
										{
											ArrayList arrayList2 = arrayList;
											int index = n - 1;
											arrayList2[index] = arrayList2[index] + " " + (string)arrayList[n];
										}
										arrayList.RemoveAt(n);
									}
								}
								this.c(string.Join("\r\n", (string[])arrayList.ToArray(typeof(string))));
								return;
							}
							if (A_1 is byte[])
							{
								this.c(Encoding.ASCII.GetString((byte[])A_1, 0, ((byte[])A_1).Length));
								return;
							}
							this.c(A_1.ToString());
							return;
						}
						else
						{
							if (!(A_0 == "3001"))
							{
								goto IL_D1E;
							}
							if (this.aa.Count > 0 && ((string[])this.aa[this.aa.Count - 1])[0] == null)
							{
								((string[])this.aa[this.aa.Count - 1])[0] = b8.a((string)A_1);
								return;
							}
							ArrayList arrayList3 = this.aa;
							string[] array3 = new string[2];
							array3[0] = b8.a((string)A_1);
							arrayList3.Add(array3);
							return;
						}
					}
					else
					{
						if (!(A_0 == "3003"))
						{
							goto IL_D1E;
						}
						if (this.aa.Count > 0 && (((string[])this.aa[this.aa.Count - 1])[1] == null || (((string[])this.aa[this.aa.Count - 1])[1] != null && ((string[])this.aa[this.aa.Count - 1])[1].IndexOf("@") == -1)))
						{
							((string[])this.aa[this.aa.Count - 1])[1] = b8.a((string)A_1);
							return;
						}
						this.aa.Add(new string[]
						{
							null,
							b8.a((string)A_1)
						});
						return;
					}
				}
				else if (num != 617210976U)
				{
					if (num != 630853140U)
					{
						if (num != 753434108U)
						{
							goto IL_D1E;
						}
						if (!(A_0 == "0065"))
						{
							goto IL_D1E;
						}
						if (this.i() == null)
						{
							this.h(b8.a((string)A_1));
							return;
						}
						return;
					}
					else if (!(A_0 == "1013"))
					{
						goto IL_D1E;
					}
				}
				else
				{
					if (!(A_0 == "39fe"))
					{
						goto IL_D1E;
					}
					if (this.aa.Count > 0 && (((string[])this.aa[this.aa.Count - 1])[1] == null || (((string[])this.aa[this.aa.Count - 1])[1] != null && ((string[])this.aa[this.aa.Count - 1])[1].IndexOf("@") == -1)))
					{
						((string[])this.aa[this.aa.Count - 1])[1] = b8.a((string)A_1);
						return;
					}
					return;
				}
				if (A_2 && this.a() != null && !(this.a() == string.Empty))
				{
					return;
				}
				if (A_1 is string)
				{
					this.i((string)A_1);
					return;
				}
				if (!(A_1 is byte[]))
				{
					this.i(A_1.ToString());
					return;
				}
				this.i(A_3.GetString((byte[])A_1, 0, ((byte[])A_1).Length));
				if (this.a().Length > 0 && this.a()[this.a().Length - 1] == '\0')
				{
					this.i(this.a().Substring(0, this.a().Length - 1));
				}
				Encoding encoding2 = bb.b(this.a());
				if (encoding2 != null && encoding2 != A_3)
				{
					this.i(encoding2.GetString((byte[])A_1, 0, ((byte[])A_1).Length));
					return;
				}
				return;
			}
			else if (num <= 2480896331U)
			{
				if (num <= 1877387486U)
				{
					if (num != 1139172250U)
					{
						if (num == 1877387486U)
						{
							if (A_0 == "007d")
							{
								string a_ = (string)A_1;
								this.l(a_);
								this.e(ba.b(a_));
								return;
							}
						}
					}
					else if (A_0 == "0070")
					{
						if (this.n() == null)
						{
							this.g(b8.a((string)A_1));
							return;
						}
						return;
					}
				}
				else if (num != 2162901199U)
				{
					if (num != 2409574944U)
					{
						if (num == 2480896331U)
						{
							if (A_0 == "0c1f")
							{
								if (this.i() == null)
								{
									this.h(b8.a((string)A_1));
									return;
								}
								return;
							}
						}
					}
					else if (A_0 == "1035")
					{
						this.j((string)A_1);
						return;
					}
				}
				else if (A_0 == "001a")
				{
					this.c = (string)A_1;
					return;
				}
			}
			else if (num <= 3203407767U)
			{
				if (num != 2564784426U)
				{
					if (num != 3152236267U)
					{
						if (num == 3203407767U)
						{
							if (A_0 == "0037")
							{
								this.g(b8.a((string)A_1));
								return;
							}
						}
					}
					else if (A_0 == "0042")
					{
						if (this.h() == null || this.t())
						{
							this.k(b8.a((string)A_1));
							this.a(false);
							return;
						}
						return;
					}
				}
				else if (A_0 == "0c1a")
				{
					if (this.h() == null || this.t())
					{
						this.k(b8.a((string)A_1));
						this.a(false);
						return;
					}
					return;
				}
			}
			else if (num != 3484203082U)
			{
				if (num != 3500980701U)
				{
					if (num == 3619815227U)
					{
						if (A_0 == "0e1d")
						{
							if (this.n() == null)
							{
								this.g(b8.a((string)A_1));
								return;
							}
							return;
						}
					}
				}
				else if (A_0 == "8000")
				{
					if (this.i() == null && A_1 is string)
					{
						this.h(b8.a((string)A_1));
						return;
					}
					return;
				}
			}
			else if (A_0 == "8001")
			{
				if (this.h() == null && A_1 is string)
				{
					this.k(b8.a((string)A_1));
					this.a(true);
					return;
				}
				return;
			}
			IL_D1E:
			if (this.o == null)
			{
				this.o = new a7();
			}
			if (!this.o.a(A_0, A_1) && this.w[A_0] != null)
			{
				this.w.Add(A_0, A_1);
			}
		}

		// Token: 0x06001465 RID: 5221 RVA: 0x0005DE14 File Offset: 0x0005CE14
		public string a(string A_0, string A_1)
		{
			if (A_0 == null && A_1 == null)
			{
				return null;
			}
			if (A_1 == null)
			{
				return A_0;
			}
			if (A_0 == null)
			{
				return A_1;
			}
			if (A_0.ToUpper().Equals(A_1.ToUpper()))
			{
				return A_0;
			}
			return string.Concat(new string[]
			{
				"\"",
				A_1,
				"\" <",
				A_0,
				">"
			});
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x0005DE73 File Offset: 0x0005CE73
		public ArrayList v()
		{
			return this.v;
		}

		// Token: 0x06001467 RID: 5223 RVA: 0x0005DE7B File Offset: 0x0005CE7B
		public void c(ArrayList A_0)
		{
			this.v = A_0;
		}

		// Token: 0x06001468 RID: 5224 RVA: 0x0005DE84 File Offset: 0x0005CE84
		public static string b(string A_0)
		{
			if (A_0 == null)
			{
				return string.Empty;
			}
			foreach (string text in A_0.Split(new char[]
			{
				'\n'
			}))
			{
				if (text.ToLower().StartsWith("date:"))
				{
					return text.Substring("Date:".Length).Trim();
				}
			}
			return string.Empty;
		}

		// Token: 0x06001469 RID: 5225 RVA: 0x0005DEEB File Offset: 0x0005CEEB
		public static bool a(string A_0)
		{
			return A_0.IndexOf("\\fromhtml") != -1;
		}

		// Token: 0x0600146A RID: 5226 RVA: 0x0005DF00 File Offset: 0x0005CF00
		public static string a(string A_0, Encoding A_1)
		{
			int i = 0;
			StringDictionary stringDictionary = new StringDictionary();
			i = A_0.IndexOf("\\fonttbl");
			if (i != -1)
			{
				int num = 0;
				int num2 = 0;
				i += 8;
				while (i < A_0.Length)
				{
					if (num == -1)
					{
						break;
					}
					if (A_0[i] == '{')
					{
						num++;
						i++;
					}
					else if (A_0[i] == '}')
					{
						num--;
						i++;
					}
					else if (num == 1 && A_0.Length >= i + 3 && A_0.Substring(i, 3) == "\\f" + num2.ToString())
					{
						i += 3;
						num2++;
					}
					else if (num == 1 && A_0.Length >= i + 9 && A_0.Substring(i, 9) == "\\fcharset")
					{
						i += 9;
						string text = string.Empty;
						while (A_0.Length > i && A_0[i] >= '0' && A_0[i] <= '9')
						{
							text += A_0[i++].ToString();
						}
						if (text != string.Empty && !stringDictionary.ContainsKey((num2 - 1).ToString()))
						{
							uint num3 = global::b.a(text);
							if (num3 <= 2000333201U)
							{
								if (num3 <= 923577301U)
								{
									if (num3 <= 837583285U)
									{
										if (num3 != 837436190U)
										{
											if (num3 == 837583285U)
											{
												if (text == "222")
												{
													stringDictionary.Add((num2 - 1).ToString(), "TIS-620");
												}
											}
										}
										else if (text == "238")
										{
											stringDictionary.Add((num2 - 1).ToString(), "windows-1250");
										}
									}
									else if (num3 != 890022063U)
									{
										if (num3 == 923577301U)
										{
											if (text == "2")
											{
												stringDictionary.Add((num2 - 1).ToString(), "Symbol");
											}
										}
									}
									else if (text == "0")
									{
										stringDictionary.Add((num2 - 1).ToString(), (A_1.CodePage == 1252) ? "windows-1252" : A_1.HeaderName);
									}
								}
								else if (num3 <= 1815632297U)
								{
									if (num3 != 1798854678U)
									{
										if (num3 == 1815632297U)
										{
											if (text == "129")
											{
												stringDictionary.Add((num2 - 1).ToString(), "Hang");
											}
										}
									}
									else if (text == "128")
									{
										stringDictionary.Add((num2 - 1).ToString(), "Sjis");
									}
								}
								else if (num3 != 1966777963U)
								{
									if (num3 == 2000333201U)
									{
										if (text == "136")
										{
											stringDictionary.Add((num2 - 1).ToString(), "ChiBIG");
										}
									}
								}
								else if (text == "134")
								{
									stringDictionary.Add((num2 - 1).ToString(), "GB2312");
								}
							}
							else if (num3 <= 3912290219U)
							{
								if (num3 <= 3219607825U)
								{
									if (num3 != 2033888439U)
									{
										if (num3 == 3219607825U)
										{
											if (text == "204")
											{
												stringDictionary.Add((num2 - 1).ToString(), "windows-1251");
											}
										}
									}
									else if (text == "130")
									{
										stringDictionary.Add((num2 - 1).ToString(), "JOHAB");
									}
								}
								else if (num3 != 3861957362U)
								{
									if (num3 == 3912290219U)
									{
										if (text == "178")
										{
											stringDictionary.Add((num2 - 1).ToString(), "windows-1256");
										}
									}
								}
								else if (text == "177")
								{
									stringDictionary.Add((num2 - 1).ToString(), "windows-1255");
								}
							}
							else if (num3 <= 4180585028U)
							{
								if (num3 != 4112488814U)
								{
									if (num3 == 4180585028U)
									{
										if (text == "162")
										{
											stringDictionary.Add((num2 - 1).ToString(), "windows-1254");
										}
									}
								}
								else if (text == "186")
								{
									stringDictionary.Add((num2 - 1).ToString(), "windows-1257");
								}
							}
							else if (num3 != 4197362647U)
							{
								if (num3 == 4230917885U)
								{
									if (text == "161")
									{
										stringDictionary.Add((num2 - 1).ToString(), "windows-1253");
									}
								}
							}
							else if (text == "163")
							{
								stringDictionary.Add((num2 - 1).ToString(), "windows-1258");
							}
						}
					}
					else
					{
						i++;
					}
				}
			}
			else
			{
				i = 0;
			}
			Encoding encoding = A_1;
			StringBuilder stringBuilder = new StringBuilder();
			while (A_0.Length >= i + 11)
			{
				if (!(A_0.Substring(i, 11) != "{\\*\\htmltag"))
				{
					break;
				}
				i++;
			}
			while (i < A_0.Length)
			{
				if (A_0[i] == '{')
				{
					i++;
				}
				else if (A_0[i] == '}')
				{
					i++;
				}
				else if (A_0.Length >= i + 10 && A_0.Substring(i, 10) == "\\*\\htmltag")
				{
					i += 10;
					int num4 = 0;
					while (A_0[i] >= '0' && A_0[i] <= '9')
					{
						num4 = num4 * 10 + (int)A_0[i] - 48;
						i++;
					}
					if (A_0[i] == ' ')
					{
						i++;
					}
				}
				else if (A_0.Length >= i + 11 && A_0.Substring(i, 11) == "\\*\\mhtmltag")
				{
					i += 11;
					int num5 = 0;
					while (A_0[i] >= '0' && A_0[i] <= '9')
					{
						num5 = num5 * 10 + (int)A_0[i] - 48;
						i++;
					}
					if (A_0[i] == ' ')
					{
						i++;
					}
					bool flag = false;
					while (i < A_0.Length && (A_0[i] != '}' || flag))
					{
						if (A_0[i] == '"')
						{
							flag = !flag;
						}
						i++;
					}
					if (A_0[i] == '}')
					{
						i++;
					}
				}
				else if (A_0.Length >= i + 5 && A_0.Substring(i, 5) == "\\pard")
				{
					stringBuilder.Append("\r\n");
					i += 5;
					if (A_0[i] == ' ')
					{
						i++;
					}
				}
				else if (A_0.Length >= i + 4 && A_0.Substring(i, 4) == "\\par")
				{
					stringBuilder.Append("\r\n");
					i += 4;
					if (A_0[i] == ' ')
					{
						i++;
					}
				}
				else if (A_0.Length >= i + 5 && A_0.Substring(i, 5) == "\\line")
				{
					stringBuilder.Append("\r\n");
					i += 5;
					if (A_0[i] == ' ')
					{
						i++;
					}
				}
				else if (A_0.Length >= i + 4 && A_0.Substring(i, 4) == "\\tab")
				{
					stringBuilder.Append("   ");
					i += 4;
					if (A_0[i] == ' ')
					{
						i++;
					}
				}
				else if (A_0.Length >= i + 7 && A_0.Substring(i, 7) == "\\lquote")
				{
					stringBuilder.Append("&#8216;");
					i += 7;
					if (A_0[i] == ' ')
					{
						i++;
					}
				}
				else if (A_0.Length >= i + 7 && A_0.Substring(i, 7) == "\\rquote")
				{
					stringBuilder.Append("&#8217;");
					i += 7;
					if (A_0[i] == ' ')
					{
						i++;
					}
				}
				else if (A_0.Length >= i + 6 && A_0.Substring(i, 6) == "\\rtlch")
				{
					i += 6;
				}
				else if (A_0.Length >= i + 6 && A_0.Substring(i, 6) == "\\ltrch")
				{
					i += 6;
				}
				else if (A_0.Length >= i + 3 && A_0.Substring(i, 3) == "\\ql")
				{
					i += 3;
				}
				else if (A_0.Length >= i + 6 && A_0.Substring(i, 6) == "\\plain")
				{
					i += 6;
				}
				else if (A_0.Length >= i + 6 && A_0.Substring(i, 6) == "\\intbl")
				{
					i += 6;
				}
				else if (A_0.Length >= i + 3 && A_0.Substring(i, 3) == "\\qj")
				{
					i += 3;
				}
				else if (A_0.Length >= i + 7 && A_0.Substring(i, 6) == "\\cbpat" && char.IsDigit(A_0[i + 6]))
				{
					i += 7;
				}
				else if (A_0.Length >= i + 4 && A_0.Substring(i, 3) == "\\sb" && char.IsDigit(A_0[i + 3]))
				{
					int num6 = 4;
					while (A_0.Length > i + num6 && char.IsDigit(A_0[i + num6]))
					{
						num6++;
					}
					i += num6;
				}
				else if (A_0.Length >= i + 4 && A_0.Substring(i, 3) == "\\ri" && char.IsDigit(A_0[i + 3]))
				{
					int num7 = 4;
					while (A_0.Length > i + num7 && char.IsDigit(A_0[i + num7]))
					{
						num7++;
					}
					i += num7;
				}
				else if (A_0.Length >= i + 4 && A_0.Substring(i, 3) == "\\fi" && char.IsDigit(A_0[i + 3]))
				{
					int num8 = 4;
					while (A_0.Length > i + num8 && char.IsDigit(A_0[i + num8]))
					{
						num8++;
					}
					i += num8;
				}
				else if (A_0.Length >= i + 3 && A_0.Substring(i, 3) == "\\f0")
				{
					i += 3;
				}
				else if (A_0.Length >= i + 3 && A_0.Substring(i, 3) == "\\li")
				{
					i += 3;
					while (A_0[i] >= '0' && A_0[i] <= '9')
					{
						i++;
					}
					if (A_0[i] == ' ')
					{
						i++;
					}
				}
				else if (A_0.Length >= i + 4 && A_0.Substring(i, 4) == "\\fi-")
				{
					i += 4;
					while (A_0[i] >= '0' && A_0[i] <= '9')
					{
						i++;
					}
					if (A_0[i] == ' ')
					{
						i++;
					}
				}
				else if (A_0.Length >= i + 9 && A_0.Substring(i, 2) == "\\u" && A_0.Substring(i + 5, 4) == "\\'f3")
				{
					stringBuilder.Append("&#");
					stringBuilder.Append(A_0.Substring(i + 2, 3));
					stringBuilder.Append(";");
					i += 9;
				}
				else if (A_0.Length >= i + 10 && A_0.Substring(i, 2) == "\\u" && A_0.Substring(i + 6, 4) == "\\'f3")
				{
					stringBuilder.Append("&#");
					stringBuilder.Append(A_0.Substring(i + 2, 4));
					stringBuilder.Append(";");
					i += 10;
				}
				else if (A_0.Length >= i + 4 && A_0.Substring(i, 2) == "\\'")
				{
					int num9 = (int)A_0[i + 2];
					int num10 = (int)A_0[i + 3];
					if ((ushort)num9 >= 48 && (ushort)num9 <= 57)
					{
						num9 -= 48;
					}
					else if ((ushort)num9 >= 65 && (ushort)num9 <= 90)
					{
						num9 = num9 - 65 + 10;
					}
					else if ((ushort)num9 >= 97 && (ushort)num9 <= 122)
					{
						num9 = num9 - 97 + 10;
					}
					if ((ushort)num10 >= 48 && (ushort)num10 <= 57)
					{
						num10 -= 48;
					}
					else if ((ushort)num10 >= 65 && (ushort)num10 <= 90)
					{
						num10 = num10 - 65 + 10;
					}
					else if ((ushort)num10 >= 97 && (ushort)num10 <= 122)
					{
						num10 = num10 - 97 + 10;
					}
					if ((encoding.CodePage == 932 || encoding.CodePage == 949 || encoding.CodePage == 1361 || encoding.CodePage == 936 || encoding.CodePage == 950) && A_0.Length >= i + 8 && A_0.Substring(i + 4, 2) == "\\'")
					{
						int num11 = (int)A_0[i + 6];
						int num12 = (int)A_0[i + 7];
						if ((ushort)num11 >= 48 && (ushort)num11 <= 57)
						{
							num11 -= 48;
						}
						else if ((ushort)num11 >= 65 && (ushort)num11 <= 90)
						{
							num11 = num11 - 65 + 10;
						}
						else if ((ushort)num11 >= 97 && (ushort)num11 <= 122)
						{
							num11 = num11 - 97 + 10;
						}
						if ((ushort)num12 >= 48 && (ushort)num12 <= 57)
						{
							num12 -= 48;
						}
						else if ((ushort)num12 >= 65 && (ushort)num12 <= 90)
						{
							num12 = num12 - 65 + 10;
						}
						else if ((ushort)num12 >= 97 && (ushort)num12 <= 122)
						{
							num12 = num12 - 97 + 10;
						}
						stringBuilder.Append(encoding.GetString(new byte[]
						{
							(byte)(num9 * 16 + num10),
							(byte)(num11 * 16 + num12)
						}, 0, 2));
						i += 8;
					}
					else
					{
						stringBuilder.Append(encoding.GetString(new byte[]
						{
							(byte)(num9 * 16 + num10)
						}, 0, 1));
						i += 4;
					}
				}
				else if (A_0.Length >= i + 7 && A_0.Substring(i, 7) == "\\pntext")
				{
					for (i += 7; i < A_0.Length; i++)
					{
						if (A_0[i] == '}')
						{
							break;
						}
					}
				}
				else if (A_0.Length >= i + 8 && A_0.Substring(i, 8) == "\\htmlrtf")
				{
					i++;
					while (A_0.Length >= i + 9 && A_0.Substring(i, 9) != "\\htmlrtf0")
					{
						if (A_0.Length >= i + 3 && A_0.Substring(i, 2) == "\\f")
						{
							int num13 = 0;
							while (A_0.Length >= i + 3 + num13 && A_0[i + 2 + num13] >= '0' && A_0[i + 2 + num13] <= '9')
							{
								num13++;
							}
							if (num13 > 0)
							{
								string key = A_0.Substring(i + 2, num13);
								if (stringDictionary[key] != null && A_0.Substring(i, 9).IndexOf('}') == -1)
								{
									try
									{
										encoding = Encoding.GetEncoding(stringDictionary[key]);
									}
									catch (ArgumentException)
									{
									}
								}
							}
						}
						i++;
					}
					if (A_0.Length >= i + 9)
					{
						i += 9;
					}
					if (A_0[i] == ' ')
					{
						i++;
					}
				}
				else if (A_0[i] == '\r' || A_0[i] == '\n')
				{
					i++;
				}
				else if (A_0.Length >= i + 2 && A_0.Substring(i, 2) == "\\{")
				{
					stringBuilder.Append('{');
					i += 2;
				}
				else if (A_0.Length >= i + 2 && A_0.Substring(i, 2) == "\\}")
				{
					stringBuilder.Append('}');
					i += 2;
				}
				else if (A_0.Length >= i + 3 && A_0.Substring(i, 3) == "\\\\)")
				{
					stringBuilder.Append(')');
					i += 3;
				}
				else if (A_0.Length >= i + 3 && A_0.Substring(i, 3) == "\\\\.")
				{
					stringBuilder.Append('.');
					i += 3;
				}
				else if (A_0.Length >= i + 2 && A_0.Substring(i, 2) == "\\\\")
				{
					stringBuilder.Append('\\');
					i += 2;
				}
				else
				{
					stringBuilder.Append(A_0[i]);
					i++;
				}
			}
			string input = stringBuilder.ToString();
			return new Regex("\\\\u(\\d{3,4}) ?\\?").Replace(input, "&#$1;");
		}

		// Token: 0x04001021 RID: 4129
		private const string a = "IPM.Note";

		// Token: 0x04001022 RID: 4130
		private const string b = "IPM.Contact";

		// Token: 0x04001023 RID: 4131
		private string c = "IPM.Note";

		// Token: 0x04001024 RID: 4132
		private string d;

		// Token: 0x04001025 RID: 4133
		private string e;

		// Token: 0x04001026 RID: 4134
		private string f;

		// Token: 0x04001027 RID: 4135
		private bool g;

		// Token: 0x04001028 RID: 4136
		private ArrayList h = new ArrayList();

		// Token: 0x04001029 RID: 4137
		private string i;

		// Token: 0x0400102A RID: 4138
		private string j;

		// Token: 0x0400102B RID: 4139
		private string k;

		// Token: 0x0400102C RID: 4140
		private byte[] l;

		// Token: 0x0400102D RID: 4141
		private string m;

		// Token: 0x0400102E RID: 4142
		private Encoding n;

		// Token: 0x0400102F RID: 4143
		private a7 o;

		// Token: 0x04001030 RID: 4144
		private DateTime p = DateTime.MinValue;

		// Token: 0x04001031 RID: 4145
		private DateTime q = DateTime.MinValue;

		// Token: 0x04001032 RID: 4146
		private DateTime r = DateTime.MinValue;

		// Token: 0x04001033 RID: 4147
		private MailPriority s;

		// Token: 0x04001034 RID: 4148
		private string t;

		// Token: 0x04001035 RID: 4149
		private string u = string.Empty;

		// Token: 0x04001036 RID: 4150
		private ArrayList v = new ArrayList();

		// Token: 0x04001037 RID: 4151
		private ListDictionary w = new ListDictionary();

		// Token: 0x04001038 RID: 4152
		private string[] x;

		// Token: 0x04001039 RID: 4153
		private string[] y;

		// Token: 0x0400103A RID: 4154
		private string[] z;

		// Token: 0x0400103B RID: 4155
		private ArrayList aa = new ArrayList();

		// Token: 0x0400103C RID: 4156
		private ArrayList ab = new ArrayList();

		// Token: 0x0400103D RID: 4157
		private ArrayList ac = new ArrayList();

		// Token: 0x0400103E RID: 4158
		private ArrayList ad = new ArrayList();
	}
}
