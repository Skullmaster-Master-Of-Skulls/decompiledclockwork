using System;
using System.Collections;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Collections;

namespace Org.BouncyCastle.Pkix
{
	// Token: 0x02000469 RID: 1129
	public class PkixNameConstraintValidator
	{
		// Token: 0x06002665 RID: 9829 RVA: 0x000E845C File Offset: 0x000E745C
		private static bool WithinDNSubtree(Asn1Sequence dns, Asn1Sequence subtree)
		{
			if (subtree.Count < 1)
			{
				return false;
			}
			if (subtree.Count > dns.Count)
			{
				return false;
			}
			for (int i = subtree.Count - 1; i >= 0; i--)
			{
				if (!subtree[i].Equals(dns[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002666 RID: 9830 RVA: 0x000E84AF File Offset: 0x000E74AF
		public void CheckPermittedDN(Asn1Sequence dns)
		{
			this.CheckPermittedDN(this.permittedSubtreesDN, dns);
		}

		// Token: 0x06002667 RID: 9831 RVA: 0x000E84BE File Offset: 0x000E74BE
		public void CheckExcludedDN(Asn1Sequence dns)
		{
			this.CheckExcludedDN(this.excludedSubtreesDN, dns);
		}

		// Token: 0x06002668 RID: 9832 RVA: 0x000E84D0 File Offset: 0x000E74D0
		private void CheckPermittedDN(ISet permitted, Asn1Sequence dns)
		{
			if (permitted == null)
			{
				return;
			}
			if (permitted.Count == 0 && dns.Count == 0)
			{
				return;
			}
			foreach (object obj in permitted)
			{
				Asn1Sequence subtree = (Asn1Sequence)obj;
				if (PkixNameConstraintValidator.WithinDNSubtree(dns, subtree))
				{
					return;
				}
			}
			throw new PkixNameConstraintValidatorException("Subject distinguished name is not from a permitted subtree");
		}

		// Token: 0x06002669 RID: 9833 RVA: 0x000E8524 File Offset: 0x000E7524
		private void CheckExcludedDN(ISet excluded, Asn1Sequence dns)
		{
			if (excluded.IsEmpty)
			{
				return;
			}
			foreach (object obj in excluded)
			{
				Asn1Sequence subtree = (Asn1Sequence)obj;
				if (PkixNameConstraintValidator.WithinDNSubtree(dns, subtree))
				{
					throw new PkixNameConstraintValidatorException("Subject distinguished name is from an excluded subtree");
				}
			}
		}

		// Token: 0x0600266A RID: 9834 RVA: 0x000E856C File Offset: 0x000E756C
		private ISet IntersectDN(ISet permitted, ISet dns)
		{
			ISet set = new HashSet();
			foreach (object obj in dns)
			{
				Asn1Sequence instance = Asn1Sequence.GetInstance(((GeneralSubtree)obj).Base.Name.ToAsn1Object());
				if (permitted == null)
				{
					if (instance != null)
					{
						set.Add(instance);
					}
				}
				else
				{
					foreach (object obj2 in permitted)
					{
						Asn1Sequence asn1Sequence = (Asn1Sequence)obj2;
						if (PkixNameConstraintValidator.WithinDNSubtree(instance, asn1Sequence))
						{
							set.Add(instance);
						}
						else if (PkixNameConstraintValidator.WithinDNSubtree(asn1Sequence, instance))
						{
							set.Add(asn1Sequence);
						}
					}
				}
			}
			return set;
		}

		// Token: 0x0600266B RID: 9835 RVA: 0x000E8604 File Offset: 0x000E7604
		private ISet UnionDN(ISet excluded, Asn1Sequence dn)
		{
			if (!excluded.IsEmpty)
			{
				ISet set = new HashSet();
				foreach (object obj in excluded)
				{
					Asn1Sequence asn1Sequence = (Asn1Sequence)obj;
					if (PkixNameConstraintValidator.WithinDNSubtree(dn, asn1Sequence))
					{
						set.Add(asn1Sequence);
					}
					else if (PkixNameConstraintValidator.WithinDNSubtree(asn1Sequence, dn))
					{
						set.Add(dn);
					}
					else
					{
						set.Add(asn1Sequence);
						set.Add(dn);
					}
				}
				return set;
			}
			if (dn == null)
			{
				return excluded;
			}
			excluded.Add(dn);
			return excluded;
		}

		// Token: 0x0600266C RID: 9836 RVA: 0x000E8680 File Offset: 0x000E7680
		private ISet IntersectEmail(ISet permitted, ISet emails)
		{
			ISet set = new HashSet();
			foreach (object obj in emails)
			{
				string text = this.ExtractNameAsString(((GeneralSubtree)obj).Base);
				if (permitted == null)
				{
					if (text != null)
					{
						set.Add(text);
					}
				}
				else
				{
					foreach (object obj2 in permitted)
					{
						string email = (string)obj2;
						this.intersectEmail(text, email, set);
					}
				}
			}
			return set;
		}

		// Token: 0x0600266D RID: 9837 RVA: 0x000E86F4 File Offset: 0x000E76F4
		private ISet UnionEmail(ISet excluded, string email)
		{
			if (!excluded.IsEmpty)
			{
				ISet set = new HashSet();
				foreach (object obj in excluded)
				{
					string email2 = (string)obj;
					this.unionEmail(email2, email, set);
				}
				return set;
			}
			if (email == null)
			{
				return excluded;
			}
			excluded.Add(email);
			return excluded;
		}

		// Token: 0x0600266E RID: 9838 RVA: 0x000E8744 File Offset: 0x000E7744
		private ISet IntersectIP(ISet permitted, ISet ips)
		{
			ISet set = new HashSet();
			foreach (object obj in ips)
			{
				byte[] octets = Asn1OctetString.GetInstance(((GeneralSubtree)obj).Base.Name).GetOctets();
				if (permitted == null)
				{
					if (octets != null)
					{
						set.Add(octets);
					}
				}
				else
				{
					foreach (object obj2 in permitted)
					{
						byte[] ipWithSubmask = (byte[])obj2;
						set.AddAll(this.IntersectIPRange(ipWithSubmask, octets));
					}
				}
			}
			return set;
		}

		// Token: 0x0600266F RID: 9839 RVA: 0x000E87C8 File Offset: 0x000E77C8
		private ISet UnionIP(ISet excluded, byte[] ip)
		{
			if (!excluded.IsEmpty)
			{
				ISet set = new HashSet();
				foreach (object obj in excluded)
				{
					byte[] ipWithSubmask = (byte[])obj;
					set.AddAll(this.UnionIPRange(ipWithSubmask, ip));
				}
				return set;
			}
			if (ip == null)
			{
				return excluded;
			}
			excluded.Add(ip);
			return excluded;
		}

		// Token: 0x06002670 RID: 9840 RVA: 0x000E8820 File Offset: 0x000E7820
		private ISet UnionIPRange(byte[] ipWithSubmask1, byte[] ipWithSubmask2)
		{
			ISet set = new HashSet();
			if (Arrays.AreEqual(ipWithSubmask1, ipWithSubmask2))
			{
				set.Add(ipWithSubmask1);
			}
			else
			{
				set.Add(ipWithSubmask1);
				set.Add(ipWithSubmask2);
			}
			return set;
		}

		// Token: 0x06002671 RID: 9841 RVA: 0x000E8854 File Offset: 0x000E7854
		private ISet IntersectIPRange(byte[] ipWithSubmask1, byte[] ipWithSubmask2)
		{
			if (ipWithSubmask1.Length != ipWithSubmask2.Length)
			{
				return new HashSet();
			}
			byte[][] array = this.ExtractIPsAndSubnetMasks(ipWithSubmask1, ipWithSubmask2);
			byte[] ip = array[0];
			byte[] array2 = array[1];
			byte[] ip2 = array[2];
			byte[] array3 = array[3];
			byte[][] array4 = this.MinMaxIPs(ip, array2, ip2, array3);
			byte[] ip3 = PkixNameConstraintValidator.Min(array4[1], array4[3]);
			byte[] ip4 = PkixNameConstraintValidator.Max(array4[0], array4[2]);
			if (PkixNameConstraintValidator.CompareTo(ip4, ip3) == 1)
			{
				return new HashSet();
			}
			byte[] ip5 = PkixNameConstraintValidator.Or(array4[0], array4[2]);
			byte[] subnetMask = PkixNameConstraintValidator.Or(array2, array3);
			return new HashSet
			{
				this.IpWithSubnetMask(ip5, subnetMask)
			};
		}

		// Token: 0x06002672 RID: 9842 RVA: 0x000E88FC File Offset: 0x000E78FC
		private byte[] IpWithSubnetMask(byte[] ip, byte[] subnetMask)
		{
			int num = ip.Length;
			byte[] array = new byte[num * 2];
			Array.Copy(ip, 0, array, 0, num);
			Array.Copy(subnetMask, 0, array, num, num);
			return array;
		}

		// Token: 0x06002673 RID: 9843 RVA: 0x000E892C File Offset: 0x000E792C
		private byte[][] ExtractIPsAndSubnetMasks(byte[] ipWithSubmask1, byte[] ipWithSubmask2)
		{
			int num = ipWithSubmask1.Length / 2;
			byte[] array = new byte[num];
			byte[] array2 = new byte[num];
			Array.Copy(ipWithSubmask1, 0, array, 0, num);
			Array.Copy(ipWithSubmask1, num, array2, 0, num);
			byte[] array3 = new byte[num];
			byte[] array4 = new byte[num];
			Array.Copy(ipWithSubmask2, 0, array3, 0, num);
			Array.Copy(ipWithSubmask2, num, array4, 0, num);
			return new byte[][]
			{
				array,
				array2,
				array3,
				array4
			};
		}

		// Token: 0x06002674 RID: 9844 RVA: 0x000E89A4 File Offset: 0x000E79A4
		private byte[][] MinMaxIPs(byte[] ip1, byte[] subnetmask1, byte[] ip2, byte[] subnetmask2)
		{
			int num = ip1.Length;
			byte[] array = new byte[num];
			byte[] array2 = new byte[num];
			byte[] array3 = new byte[num];
			byte[] array4 = new byte[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = (ip1[i] & subnetmask1[i]);
				array2[i] = ((ip1[i] & subnetmask1[i]) | ~subnetmask1[i]);
				array3[i] = (ip2[i] & subnetmask2[i]);
				array4[i] = ((ip2[i] & subnetmask2[i]) | ~subnetmask2[i]);
			}
			return new byte[][]
			{
				array,
				array2,
				array3,
				array4
			};
		}

		// Token: 0x06002675 RID: 9845 RVA: 0x000E8A4C File Offset: 0x000E7A4C
		private void CheckPermittedEmail(ISet permitted, string email)
		{
			if (permitted == null)
			{
				return;
			}
			foreach (object obj in permitted)
			{
				string constraint = (string)obj;
				if (this.EmailIsConstrained(email, constraint))
				{
					return;
				}
			}
			if (email.Length == 0 && permitted.Count == 0)
			{
				return;
			}
			throw new PkixNameConstraintValidatorException("Subject email address is not from a permitted subtree.");
		}

		// Token: 0x06002676 RID: 9846 RVA: 0x000E8AA0 File Offset: 0x000E7AA0
		private void CheckExcludedEmail(ISet excluded, string email)
		{
			if (excluded.IsEmpty)
			{
				return;
			}
			foreach (object obj in excluded)
			{
				string constraint = (string)obj;
				if (this.EmailIsConstrained(email, constraint))
				{
					throw new PkixNameConstraintValidatorException("Email address is from an excluded subtree.");
				}
			}
		}

		// Token: 0x06002677 RID: 9847 RVA: 0x000E8AE8 File Offset: 0x000E7AE8
		private void CheckPermittedIP(ISet permitted, byte[] ip)
		{
			if (permitted == null)
			{
				return;
			}
			foreach (object obj in permitted)
			{
				byte[] constraint = (byte[])obj;
				if (this.IsIPConstrained(ip, constraint))
				{
					return;
				}
			}
			if (ip.Length == 0 && permitted.Count == 0)
			{
				return;
			}
			throw new PkixNameConstraintValidatorException("IP is not from a permitted subtree.");
		}

		// Token: 0x06002678 RID: 9848 RVA: 0x000E8B3C File Offset: 0x000E7B3C
		private void checkExcludedIP(ISet excluded, byte[] ip)
		{
			if (excluded.IsEmpty)
			{
				return;
			}
			foreach (object obj in excluded)
			{
				byte[] constraint = (byte[])obj;
				if (this.IsIPConstrained(ip, constraint))
				{
					throw new PkixNameConstraintValidatorException("IP is from an excluded subtree.");
				}
			}
		}

		// Token: 0x06002679 RID: 9849 RVA: 0x000E8B84 File Offset: 0x000E7B84
		private bool IsIPConstrained(byte[] ip, byte[] constraint)
		{
			int num = ip.Length;
			if (num != constraint.Length / 2)
			{
				return false;
			}
			byte[] array = new byte[num];
			Array.Copy(constraint, num, array, 0, num);
			byte[] array2 = new byte[num];
			byte[] array3 = new byte[num];
			for (int i = 0; i < num; i++)
			{
				array2[i] = (constraint[i] & array[i]);
				array3[i] = (ip[i] & array[i]);
			}
			return Arrays.AreEqual(array2, array3);
		}

		// Token: 0x0600267A RID: 9850 RVA: 0x000E8BF4 File Offset: 0x000E7BF4
		private bool EmailIsConstrained(string email, string constraint)
		{
			string text = email.Substring(email.IndexOf('@') + 1);
			if (constraint.IndexOf('@') != -1)
			{
				if (email.ToUpper().Equals(constraint.ToUpper()))
				{
					return true;
				}
			}
			else if (!constraint[0].Equals('.'))
			{
				if (text.ToUpper().Equals(constraint.ToUpper()))
				{
					return true;
				}
			}
			else if (this.WithinDomain(text, constraint))
			{
				return true;
			}
			return false;
		}

		// Token: 0x0600267B RID: 9851 RVA: 0x000E8C68 File Offset: 0x000E7C68
		private bool WithinDomain(string testDomain, string domain)
		{
			string text = domain;
			if (text.StartsWith("."))
			{
				text = text.Substring(1);
			}
			string[] array = text.Split(new char[]
			{
				'.'
			});
			string[] array2 = testDomain.Split(new char[]
			{
				'.'
			});
			if (array2.Length <= array.Length)
			{
				return false;
			}
			int num = array2.Length - array.Length;
			for (int i = -1; i < array.Length; i++)
			{
				if (i == -1)
				{
					if (array2[i + num].Equals(""))
					{
						return false;
					}
				}
				else if (string.Compare(array2[i + num], array[i], true) != 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600267C RID: 9852 RVA: 0x000E8D0C File Offset: 0x000E7D0C
		private void CheckPermittedDNS(ISet permitted, string dns)
		{
			if (permitted == null)
			{
				return;
			}
			foreach (object obj in permitted)
			{
				string text = (string)obj;
				if (this.WithinDomain(dns, text) || dns.ToUpper().Equals(text.ToUpper()))
				{
					return;
				}
			}
			if (dns.Length == 0 && permitted.Count == 0)
			{
				return;
			}
			throw new PkixNameConstraintValidatorException("DNS is not from a permitted subtree.");
		}

		// Token: 0x0600267D RID: 9853 RVA: 0x000E8D74 File Offset: 0x000E7D74
		private void checkExcludedDNS(ISet excluded, string dns)
		{
			if (excluded.IsEmpty)
			{
				return;
			}
			foreach (object obj in excluded)
			{
				string text = (string)obj;
				if (this.WithinDomain(dns, text) || string.Compare(dns, text, true) == 0)
				{
					throw new PkixNameConstraintValidatorException("DNS is from an excluded subtree.");
				}
			}
		}

		// Token: 0x0600267E RID: 9854 RVA: 0x000E8DC8 File Offset: 0x000E7DC8
		private void unionEmail(string email1, string email2, ISet union)
		{
			if (email1.IndexOf('@') != -1)
			{
				string text = email1.Substring(email1.IndexOf('@') + 1);
				if (email2.IndexOf('@') != -1)
				{
					if (string.Compare(email1, email2, true) == 0)
					{
						union.Add(email1);
						return;
					}
					union.Add(email1);
					union.Add(email2);
					return;
				}
				else if (email2.StartsWith("."))
				{
					if (this.WithinDomain(text, email2))
					{
						union.Add(email2);
						return;
					}
					union.Add(email1);
					union.Add(email2);
					return;
				}
				else
				{
					if (string.Compare(text, email2, true) == 0)
					{
						union.Add(email2);
						return;
					}
					union.Add(email1);
					union.Add(email2);
					return;
				}
			}
			else if (email1.StartsWith("."))
			{
				if (email2.IndexOf('@') != -1)
				{
					string testDomain = email2.Substring(email1.IndexOf('@') + 1);
					if (this.WithinDomain(testDomain, email1))
					{
						union.Add(email1);
						return;
					}
					union.Add(email1);
					union.Add(email2);
					return;
				}
				else if (email2.StartsWith("."))
				{
					if (this.WithinDomain(email1, email2) || string.Compare(email1, email2, true) == 0)
					{
						union.Add(email2);
						return;
					}
					if (this.WithinDomain(email2, email1))
					{
						union.Add(email1);
						return;
					}
					union.Add(email1);
					union.Add(email2);
					return;
				}
				else
				{
					if (this.WithinDomain(email2, email1))
					{
						union.Add(email1);
						return;
					}
					union.Add(email1);
					union.Add(email2);
					return;
				}
			}
			else if (email2.IndexOf('@') != -1)
			{
				string strA = email2.Substring(email1.IndexOf('@') + 1);
				if (string.Compare(strA, email1, true) == 0)
				{
					union.Add(email1);
					return;
				}
				union.Add(email1);
				union.Add(email2);
				return;
			}
			else if (email2.StartsWith("."))
			{
				if (this.WithinDomain(email1, email2))
				{
					union.Add(email2);
					return;
				}
				union.Add(email1);
				union.Add(email2);
				return;
			}
			else
			{
				if (string.Compare(email1, email2, true) == 0)
				{
					union.Add(email1);
					return;
				}
				union.Add(email1);
				union.Add(email2);
				return;
			}
		}

		// Token: 0x0600267F RID: 9855 RVA: 0x000E8FB4 File Offset: 0x000E7FB4
		private void unionURI(string email1, string email2, ISet union)
		{
			if (email1.IndexOf('@') != -1)
			{
				string text = email1.Substring(email1.IndexOf('@') + 1);
				if (email2.IndexOf('@') != -1)
				{
					if (string.Compare(email1, email2, true) == 0)
					{
						union.Add(email1);
						return;
					}
					union.Add(email1);
					union.Add(email2);
					return;
				}
				else if (email2.StartsWith("."))
				{
					if (this.WithinDomain(text, email2))
					{
						union.Add(email2);
						return;
					}
					union.Add(email1);
					union.Add(email2);
					return;
				}
				else
				{
					if (string.Compare(text, email2, true) == 0)
					{
						union.Add(email2);
						return;
					}
					union.Add(email1);
					union.Add(email2);
					return;
				}
			}
			else if (email1.StartsWith("."))
			{
				if (email2.IndexOf('@') != -1)
				{
					string testDomain = email2.Substring(email1.IndexOf('@') + 1);
					if (this.WithinDomain(testDomain, email1))
					{
						union.Add(email1);
						return;
					}
					union.Add(email1);
					union.Add(email2);
					return;
				}
				else if (email2.StartsWith("."))
				{
					if (this.WithinDomain(email1, email2) || string.Compare(email1, email2, true) == 0)
					{
						union.Add(email2);
						return;
					}
					if (this.WithinDomain(email2, email1))
					{
						union.Add(email1);
						return;
					}
					union.Add(email1);
					union.Add(email2);
					return;
				}
				else
				{
					if (this.WithinDomain(email2, email1))
					{
						union.Add(email1);
						return;
					}
					union.Add(email1);
					union.Add(email2);
					return;
				}
			}
			else if (email2.IndexOf('@') != -1)
			{
				string strA = email2.Substring(email1.IndexOf('@') + 1);
				if (string.Compare(strA, email1, true) == 0)
				{
					union.Add(email1);
					return;
				}
				union.Add(email1);
				union.Add(email2);
				return;
			}
			else if (email2.StartsWith("."))
			{
				if (this.WithinDomain(email1, email2))
				{
					union.Add(email2);
					return;
				}
				union.Add(email1);
				union.Add(email2);
				return;
			}
			else
			{
				if (string.Compare(email1, email2, true) == 0)
				{
					union.Add(email1);
					return;
				}
				union.Add(email1);
				union.Add(email2);
				return;
			}
		}

		// Token: 0x06002680 RID: 9856 RVA: 0x000E91A0 File Offset: 0x000E81A0
		private ISet intersectDNS(ISet permitted, ISet dnss)
		{
			ISet set = new HashSet();
			foreach (object obj in dnss)
			{
				string text = this.ExtractNameAsString(((GeneralSubtree)obj).Base);
				if (permitted == null)
				{
					if (text != null)
					{
						set.Add(text);
					}
				}
				else
				{
					foreach (object obj2 in permitted)
					{
						string text2 = (string)obj2;
						if (this.WithinDomain(text2, text))
						{
							set.Add(text2);
						}
						else if (this.WithinDomain(text, text2))
						{
							set.Add(text);
						}
					}
				}
			}
			return set;
		}

		// Token: 0x06002681 RID: 9857 RVA: 0x000E9230 File Offset: 0x000E8230
		protected ISet unionDNS(ISet excluded, string dns)
		{
			if (!excluded.IsEmpty)
			{
				ISet set = new HashSet();
				foreach (object obj in excluded)
				{
					string text = (string)obj;
					if (this.WithinDomain(text, dns))
					{
						set.Add(dns);
					}
					else if (this.WithinDomain(dns, text))
					{
						set.Add(text);
					}
					else
					{
						set.Add(text);
						set.Add(dns);
					}
				}
				return set;
			}
			if (dns == null)
			{
				return excluded;
			}
			excluded.Add(dns);
			return excluded;
		}

		// Token: 0x06002682 RID: 9858 RVA: 0x000E92AC File Offset: 0x000E82AC
		private void intersectEmail(string email1, string email2, ISet intersect)
		{
			if (email1.IndexOf('@') != -1)
			{
				string text = email1.Substring(email1.IndexOf('@') + 1);
				if (email2.IndexOf('@') != -1)
				{
					if (string.Compare(email1, email2, true) == 0)
					{
						intersect.Add(email1);
						return;
					}
				}
				else if (email2.StartsWith("."))
				{
					if (this.WithinDomain(text, email2))
					{
						intersect.Add(email1);
						return;
					}
				}
				else if (string.Compare(text, email2, true) == 0)
				{
					intersect.Add(email1);
					return;
				}
			}
			else if (email1.StartsWith("."))
			{
				if (email2.IndexOf('@') != -1)
				{
					string testDomain = email2.Substring(email1.IndexOf('@') + 1);
					if (this.WithinDomain(testDomain, email1))
					{
						intersect.Add(email2);
						return;
					}
				}
				else if (email2.StartsWith("."))
				{
					if (this.WithinDomain(email1, email2) || string.Compare(email1, email2, true) == 0)
					{
						intersect.Add(email1);
						return;
					}
					if (this.WithinDomain(email2, email1))
					{
						intersect.Add(email2);
						return;
					}
				}
				else if (this.WithinDomain(email2, email1))
				{
					intersect.Add(email2);
					return;
				}
			}
			else if (email2.IndexOf('@') != -1)
			{
				string strA = email2.Substring(email2.IndexOf('@') + 1);
				if (string.Compare(strA, email1, true) == 0)
				{
					intersect.Add(email2);
					return;
				}
			}
			else if (email2.StartsWith("."))
			{
				if (this.WithinDomain(email1, email2))
				{
					intersect.Add(email1);
					return;
				}
			}
			else if (string.Compare(email1, email2, true) == 0)
			{
				intersect.Add(email1);
			}
		}

		// Token: 0x06002683 RID: 9859 RVA: 0x000E9418 File Offset: 0x000E8418
		private void checkExcludedURI(ISet excluded, string uri)
		{
			if (excluded.IsEmpty)
			{
				return;
			}
			foreach (object obj in excluded)
			{
				string constraint = (string)obj;
				if (this.IsUriConstrained(uri, constraint))
				{
					throw new PkixNameConstraintValidatorException("URI is from an excluded subtree.");
				}
			}
		}

		// Token: 0x06002684 RID: 9860 RVA: 0x000E9460 File Offset: 0x000E8460
		private ISet intersectURI(ISet permitted, ISet uris)
		{
			ISet set = new HashSet();
			foreach (object obj in uris)
			{
				string text = this.ExtractNameAsString(((GeneralSubtree)obj).Base);
				if (permitted == null)
				{
					if (text != null)
					{
						set.Add(text);
					}
				}
				else
				{
					foreach (object obj2 in permitted)
					{
						string email = (string)obj2;
						this.intersectURI(email, text, set);
					}
				}
			}
			return set;
		}

		// Token: 0x06002685 RID: 9861 RVA: 0x000E94D4 File Offset: 0x000E84D4
		private ISet unionURI(ISet excluded, string uri)
		{
			if (!excluded.IsEmpty)
			{
				ISet set = new HashSet();
				foreach (object obj in excluded)
				{
					string email = (string)obj;
					this.unionURI(email, uri, set);
				}
				return set;
			}
			if (uri == null)
			{
				return excluded;
			}
			excluded.Add(uri);
			return excluded;
		}

		// Token: 0x06002686 RID: 9862 RVA: 0x000E9524 File Offset: 0x000E8524
		private void intersectURI(string email1, string email2, ISet intersect)
		{
			if (email1.IndexOf('@') != -1)
			{
				string text = email1.Substring(email1.IndexOf('@') + 1);
				if (email2.IndexOf('@') != -1)
				{
					if (string.Compare(email1, email2, true) == 0)
					{
						intersect.Add(email1);
						return;
					}
				}
				else if (email2.StartsWith("."))
				{
					if (this.WithinDomain(text, email2))
					{
						intersect.Add(email1);
						return;
					}
				}
				else if (string.Compare(text, email2, true) == 0)
				{
					intersect.Add(email1);
					return;
				}
			}
			else if (email1.StartsWith("."))
			{
				if (email2.IndexOf('@') != -1)
				{
					string testDomain = email2.Substring(email1.IndexOf('@') + 1);
					if (this.WithinDomain(testDomain, email1))
					{
						intersect.Add(email2);
						return;
					}
				}
				else if (email2.StartsWith("."))
				{
					if (this.WithinDomain(email1, email2) || string.Compare(email1, email2, true) == 0)
					{
						intersect.Add(email1);
						return;
					}
					if (this.WithinDomain(email2, email1))
					{
						intersect.Add(email2);
						return;
					}
				}
				else if (this.WithinDomain(email2, email1))
				{
					intersect.Add(email2);
					return;
				}
			}
			else if (email2.IndexOf('@') != -1)
			{
				string strA = email2.Substring(email2.IndexOf('@') + 1);
				if (string.Compare(strA, email1, true) == 0)
				{
					intersect.Add(email2);
					return;
				}
			}
			else if (email2.StartsWith("."))
			{
				if (this.WithinDomain(email1, email2))
				{
					intersect.Add(email1);
					return;
				}
			}
			else if (string.Compare(email1, email2, true) == 0)
			{
				intersect.Add(email1);
			}
		}

		// Token: 0x06002687 RID: 9863 RVA: 0x000E9690 File Offset: 0x000E8690
		private void CheckPermittedURI(ISet permitted, string uri)
		{
			if (permitted == null)
			{
				return;
			}
			foreach (object obj in permitted)
			{
				string constraint = (string)obj;
				if (this.IsUriConstrained(uri, constraint))
				{
					return;
				}
			}
			if (uri.Length == 0 && permitted.Count == 0)
			{
				return;
			}
			throw new PkixNameConstraintValidatorException("URI is not from a permitted subtree.");
		}

		// Token: 0x06002688 RID: 9864 RVA: 0x000E96E4 File Offset: 0x000E86E4
		private bool IsUriConstrained(string uri, string constraint)
		{
			string text = PkixNameConstraintValidator.ExtractHostFromURL(uri);
			if (!constraint.StartsWith("."))
			{
				if (string.Compare(text, constraint, true) == 0)
				{
					return true;
				}
			}
			else if (this.WithinDomain(text, constraint))
			{
				return true;
			}
			return false;
		}

		// Token: 0x06002689 RID: 9865 RVA: 0x000E9720 File Offset: 0x000E8720
		private static string ExtractHostFromURL(string url)
		{
			string text = url.Substring(url.IndexOf(':') + 1);
			if (text.IndexOf("//") != -1)
			{
				text = text.Substring(text.IndexOf("//") + 2);
			}
			if (text.LastIndexOf(':') != -1)
			{
				text = text.Substring(0, text.LastIndexOf(':'));
			}
			text = text.Substring(text.IndexOf(':') + 1);
			text = text.Substring(text.IndexOf('@') + 1);
			if (text.IndexOf('/') != -1)
			{
				text = text.Substring(0, text.IndexOf('/'));
			}
			return text;
		}

		// Token: 0x0600268A RID: 9866 RVA: 0x000E97BC File Offset: 0x000E87BC
		public void checkPermitted(GeneralName name)
		{
			switch (name.TagNo)
			{
			case 1:
				this.CheckPermittedEmail(this.permittedSubtreesEmail, this.ExtractNameAsString(name));
				return;
			case 2:
				this.CheckPermittedDNS(this.permittedSubtreesDNS, DerIA5String.GetInstance(name.Name).GetString());
				return;
			case 3:
			case 5:
				break;
			case 4:
				this.CheckPermittedDN(Asn1Sequence.GetInstance(name.Name.ToAsn1Object()));
				return;
			case 6:
				this.CheckPermittedURI(this.permittedSubtreesURI, DerIA5String.GetInstance(name.Name).GetString());
				return;
			case 7:
			{
				byte[] octets = Asn1OctetString.GetInstance(name.Name).GetOctets();
				this.CheckPermittedIP(this.permittedSubtreesIP, octets);
				break;
			}
			default:
				return;
			}
		}

		// Token: 0x0600268B RID: 9867 RVA: 0x000E9878 File Offset: 0x000E8878
		public void checkExcluded(GeneralName name)
		{
			switch (name.TagNo)
			{
			case 1:
				this.CheckExcludedEmail(this.excludedSubtreesEmail, this.ExtractNameAsString(name));
				return;
			case 2:
				this.checkExcludedDNS(this.excludedSubtreesDNS, DerIA5String.GetInstance(name.Name).GetString());
				return;
			case 3:
			case 5:
				break;
			case 4:
				this.CheckExcludedDN(Asn1Sequence.GetInstance(name.Name.ToAsn1Object()));
				return;
			case 6:
				this.checkExcludedURI(this.excludedSubtreesURI, DerIA5String.GetInstance(name.Name).GetString());
				return;
			case 7:
			{
				byte[] octets = Asn1OctetString.GetInstance(name.Name).GetOctets();
				this.checkExcludedIP(this.excludedSubtreesIP, octets);
				break;
			}
			default:
				return;
			}
		}

		// Token: 0x0600268C RID: 9868 RVA: 0x000E9934 File Offset: 0x000E8934
		public void IntersectPermittedSubtree(Asn1Sequence permitted)
		{
			IDictionary dictionary = new Hashtable();
			foreach (object obj in permitted)
			{
				GeneralSubtree instance = GeneralSubtree.GetInstance(obj);
				int tagNo = instance.Base.TagNo;
				if (dictionary[tagNo] == null)
				{
					dictionary[tagNo] = new HashSet();
				}
				((ISet)dictionary[tagNo]).Add(instance);
			}
			foreach (object obj2 in dictionary)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj2;
				switch ((int)dictionaryEntry.Key)
				{
				case 1:
					this.permittedSubtreesEmail = this.IntersectEmail(this.permittedSubtreesEmail, (ISet)dictionaryEntry.Value);
					break;
				case 2:
					this.permittedSubtreesDNS = this.intersectDNS(this.permittedSubtreesDNS, (ISet)dictionaryEntry.Value);
					break;
				case 4:
					this.permittedSubtreesDN = this.IntersectDN(this.permittedSubtreesDN, (ISet)dictionaryEntry.Value);
					break;
				case 6:
					this.permittedSubtreesURI = this.intersectURI(this.permittedSubtreesURI, (ISet)dictionaryEntry.Value);
					break;
				case 7:
					this.permittedSubtreesIP = this.IntersectIP(this.permittedSubtreesIP, (ISet)dictionaryEntry.Value);
					break;
				}
			}
		}

		// Token: 0x0600268D RID: 9869 RVA: 0x000E9AA3 File Offset: 0x000E8AA3
		private string ExtractNameAsString(GeneralName name)
		{
			return DerIA5String.GetInstance(name.Name).GetString();
		}

		// Token: 0x0600268E RID: 9870 RVA: 0x000E9AB8 File Offset: 0x000E8AB8
		public void IntersectEmptyPermittedSubtree(int nameType)
		{
			switch (nameType)
			{
			case 1:
				this.permittedSubtreesEmail = new HashSet();
				return;
			case 2:
				this.permittedSubtreesDNS = new HashSet();
				return;
			case 3:
			case 5:
				break;
			case 4:
				this.permittedSubtreesDN = new HashSet();
				return;
			case 6:
				this.permittedSubtreesURI = new HashSet();
				return;
			case 7:
				this.permittedSubtreesIP = new HashSet();
				break;
			default:
				return;
			}
		}

		// Token: 0x0600268F RID: 9871 RVA: 0x000E9B28 File Offset: 0x000E8B28
		public void AddExcludedSubtree(GeneralSubtree subtree)
		{
			GeneralName @base = subtree.Base;
			switch (@base.TagNo)
			{
			case 1:
				this.excludedSubtreesEmail = this.UnionEmail(this.excludedSubtreesEmail, this.ExtractNameAsString(@base));
				return;
			case 2:
				this.excludedSubtreesDNS = this.unionDNS(this.excludedSubtreesDNS, this.ExtractNameAsString(@base));
				return;
			case 3:
			case 5:
				break;
			case 4:
				this.excludedSubtreesDN = this.UnionDN(this.excludedSubtreesDN, (Asn1Sequence)@base.Name.ToAsn1Object());
				return;
			case 6:
				this.excludedSubtreesURI = this.unionURI(this.excludedSubtreesURI, this.ExtractNameAsString(@base));
				return;
			case 7:
				this.excludedSubtreesIP = this.UnionIP(this.excludedSubtreesIP, Asn1OctetString.GetInstance(@base.Name).GetOctets());
				break;
			default:
				return;
			}
		}

		// Token: 0x06002690 RID: 9872 RVA: 0x000E9BFC File Offset: 0x000E8BFC
		private static byte[] Max(byte[] ip1, byte[] ip2)
		{
			for (int i = 0; i < ip1.Length; i++)
			{
				if (((int)ip1[i] & 65535) > ((int)ip2[i] & 65535))
				{
					return ip1;
				}
			}
			return ip2;
		}

		// Token: 0x06002691 RID: 9873 RVA: 0x000E9C30 File Offset: 0x000E8C30
		private static byte[] Min(byte[] ip1, byte[] ip2)
		{
			for (int i = 0; i < ip1.Length; i++)
			{
				if (((int)ip1[i] & 65535) < ((int)ip2[i] & 65535))
				{
					return ip1;
				}
			}
			return ip2;
		}

		// Token: 0x06002692 RID: 9874 RVA: 0x000E9C62 File Offset: 0x000E8C62
		private static int CompareTo(byte[] ip1, byte[] ip2)
		{
			if (Arrays.AreEqual(ip1, ip2))
			{
				return 0;
			}
			if (Arrays.AreEqual(PkixNameConstraintValidator.Max(ip1, ip2), ip1))
			{
				return 1;
			}
			return -1;
		}

		// Token: 0x06002693 RID: 9875 RVA: 0x000E9C84 File Offset: 0x000E8C84
		private static byte[] Or(byte[] ip1, byte[] ip2)
		{
			byte[] array = new byte[ip1.Length];
			for (int i = 0; i < ip1.Length; i++)
			{
				array[i] = (ip1[i] | ip2[i]);
			}
			return array;
		}

		// Token: 0x06002694 RID: 9876 RVA: 0x000E9CB4 File Offset: 0x000E8CB4
		[Obsolete("Use GetHashCode instead")]
		public int HashCode()
		{
			return this.GetHashCode();
		}

		// Token: 0x06002695 RID: 9877 RVA: 0x000E9CBC File Offset: 0x000E8CBC
		public override int GetHashCode()
		{
			return this.HashCollection(this.excludedSubtreesDN) + this.HashCollection(this.excludedSubtreesDNS) + this.HashCollection(this.excludedSubtreesEmail) + this.HashCollection(this.excludedSubtreesIP) + this.HashCollection(this.excludedSubtreesURI) + this.HashCollection(this.permittedSubtreesDN) + this.HashCollection(this.permittedSubtreesDNS) + this.HashCollection(this.permittedSubtreesEmail) + this.HashCollection(this.permittedSubtreesIP) + this.HashCollection(this.permittedSubtreesURI);
		}

		// Token: 0x06002696 RID: 9878 RVA: 0x000E9D4C File Offset: 0x000E8D4C
		private int HashCollection(ICollection coll)
		{
			if (coll == null)
			{
				return 0;
			}
			int num = 0;
			foreach (object obj in coll)
			{
				if (obj is byte[])
				{
					num += Arrays.GetHashCode((byte[])obj);
				}
				else
				{
					num += obj.GetHashCode();
				}
			}
			return num;
		}

		// Token: 0x06002697 RID: 9879 RVA: 0x000E9D9C File Offset: 0x000E8D9C
		public override bool Equals(object o)
		{
			if (!(o is PkixNameConstraintValidator))
			{
				return false;
			}
			PkixNameConstraintValidator pkixNameConstraintValidator = (PkixNameConstraintValidator)o;
			return this.CollectionsAreEqual(pkixNameConstraintValidator.excludedSubtreesDN, this.excludedSubtreesDN) && this.CollectionsAreEqual(pkixNameConstraintValidator.excludedSubtreesDNS, this.excludedSubtreesDNS) && this.CollectionsAreEqual(pkixNameConstraintValidator.excludedSubtreesEmail, this.excludedSubtreesEmail) && this.CollectionsAreEqual(pkixNameConstraintValidator.excludedSubtreesIP, this.excludedSubtreesIP) && this.CollectionsAreEqual(pkixNameConstraintValidator.excludedSubtreesURI, this.excludedSubtreesURI) && this.CollectionsAreEqual(pkixNameConstraintValidator.permittedSubtreesDN, this.permittedSubtreesDN) && this.CollectionsAreEqual(pkixNameConstraintValidator.permittedSubtreesDNS, this.permittedSubtreesDNS) && this.CollectionsAreEqual(pkixNameConstraintValidator.permittedSubtreesEmail, this.permittedSubtreesEmail) && this.CollectionsAreEqual(pkixNameConstraintValidator.permittedSubtreesIP, this.permittedSubtreesIP) && this.CollectionsAreEqual(pkixNameConstraintValidator.permittedSubtreesURI, this.permittedSubtreesURI);
		}

		// Token: 0x06002698 RID: 9880 RVA: 0x000E9E8C File Offset: 0x000E8E8C
		private bool CollectionsAreEqual(ICollection coll1, ICollection coll2)
		{
			if (coll1 == coll2)
			{
				return true;
			}
			if (coll1 == null || coll2 == null)
			{
				return false;
			}
			if (coll1.Count != coll2.Count)
			{
				return false;
			}
			foreach (object o in coll1)
			{
				IEnumerator enumerator2 = coll2.GetEnumerator();
				bool flag = false;
				while (enumerator2.MoveNext())
				{
					object o2 = enumerator2.Current;
					if (this.SpecialEquals(o, o2))
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
			return true;
		}

		// Token: 0x06002699 RID: 9881 RVA: 0x000E9EFF File Offset: 0x000E8EFF
		private bool SpecialEquals(object o1, object o2)
		{
			if (o1 == o2)
			{
				return true;
			}
			if (o1 == null || o2 == null)
			{
				return false;
			}
			if (o1 is byte[] && o2 is byte[])
			{
				return Arrays.AreEqual((byte[])o1, (byte[])o2);
			}
			return o1.Equals(o2);
		}

		// Token: 0x0600269A RID: 9882 RVA: 0x000E9F38 File Offset: 0x000E8F38
		private string StringifyIP(byte[] ip)
		{
			string text = "";
			for (int i = 0; i < ip.Length / 2; i++)
			{
				text = text + (int)(ip[i] & byte.MaxValue) + ".";
			}
			text = text.Substring(0, text.Length - 1);
			text += "/";
			for (int j = ip.Length / 2; j < ip.Length; j++)
			{
				text = text + (int)(ip[j] & byte.MaxValue) + ".";
			}
			return text.Substring(0, text.Length - 1);
		}

		// Token: 0x0600269B RID: 9883 RVA: 0x000E9FD0 File Offset: 0x000E8FD0
		private string StringifyIPCollection(ISet ips)
		{
			string text = "";
			text += "[";
			foreach (object obj in ips)
			{
				text = text + this.StringifyIP((byte[])obj) + ",";
			}
			if (text.Length > 1)
			{
				text = text.Substring(0, text.Length - 1);
			}
			return text + "]";
		}

		// Token: 0x0600269C RID: 9884 RVA: 0x000EA044 File Offset: 0x000E9044
		public override string ToString()
		{
			string text = "";
			text += "permitted:\n";
			if (this.permittedSubtreesDN != null)
			{
				text += "DN:\n";
				text = text + this.permittedSubtreesDN.ToString() + "\n";
			}
			if (this.permittedSubtreesDNS != null)
			{
				text += "DNS:\n";
				text = text + this.permittedSubtreesDNS.ToString() + "\n";
			}
			if (this.permittedSubtreesEmail != null)
			{
				text += "Email:\n";
				text = text + this.permittedSubtreesEmail.ToString() + "\n";
			}
			if (this.permittedSubtreesURI != null)
			{
				text += "URI:\n";
				text = text + this.permittedSubtreesURI.ToString() + "\n";
			}
			if (this.permittedSubtreesIP != null)
			{
				text += "IP:\n";
				text = text + this.StringifyIPCollection(this.permittedSubtreesIP) + "\n";
			}
			text += "excluded:\n";
			if (!this.excludedSubtreesDN.IsEmpty)
			{
				text += "DN:\n";
				text = text + this.excludedSubtreesDN.ToString() + "\n";
			}
			if (!this.excludedSubtreesDNS.IsEmpty)
			{
				text += "DNS:\n";
				text = text + this.excludedSubtreesDNS.ToString() + "\n";
			}
			if (!this.excludedSubtreesEmail.IsEmpty)
			{
				text += "Email:\n";
				text = text + this.excludedSubtreesEmail.ToString() + "\n";
			}
			if (!this.excludedSubtreesURI.IsEmpty)
			{
				text += "URI:\n";
				text = text + this.excludedSubtreesURI.ToString() + "\n";
			}
			if (!this.excludedSubtreesIP.IsEmpty)
			{
				text += "IP:\n";
				text = text + this.StringifyIPCollection(this.excludedSubtreesIP) + "\n";
			}
			return text;
		}

		// Token: 0x04001AA6 RID: 6822
		private ISet excludedSubtreesDN = new HashSet();

		// Token: 0x04001AA7 RID: 6823
		private ISet excludedSubtreesDNS = new HashSet();

		// Token: 0x04001AA8 RID: 6824
		private ISet excludedSubtreesEmail = new HashSet();

		// Token: 0x04001AA9 RID: 6825
		private ISet excludedSubtreesURI = new HashSet();

		// Token: 0x04001AAA RID: 6826
		private ISet excludedSubtreesIP = new HashSet();

		// Token: 0x04001AAB RID: 6827
		private ISet permittedSubtreesDN;

		// Token: 0x04001AAC RID: 6828
		private ISet permittedSubtreesDNS;

		// Token: 0x04001AAD RID: 6829
		private ISet permittedSubtreesEmail;

		// Token: 0x04001AAE RID: 6830
		private ISet permittedSubtreesURI;

		// Token: 0x04001AAF RID: 6831
		private ISet permittedSubtreesIP;
	}
}
