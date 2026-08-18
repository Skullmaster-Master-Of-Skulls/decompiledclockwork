using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.AccessControl;
using System.Security.Util;

namespace System.Security.Permissions
{
	// Token: 0x0200062D RID: 1581
	[ComVisible(true)]
	[Serializable]
	public sealed class FileIOPermission : CodeAccessPermission, IUnrestrictedPermission, IBuiltInPermission
	{
		// Token: 0x060038ED RID: 14573 RVA: 0x000BFB52 File Offset: 0x000BEB52
		public FileIOPermission(PermissionState state)
		{
			if (state == PermissionState.Unrestricted)
			{
				this.m_unrestricted = true;
				return;
			}
			if (state == PermissionState.None)
			{
				this.m_unrestricted = false;
				return;
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPermissionState"));
		}

		// Token: 0x060038EE RID: 14574 RVA: 0x000BFB80 File Offset: 0x000BEB80
		public FileIOPermission(FileIOPermissionAccess access, string path)
		{
			this.VerifyAccess(access);
			string[] pathListOrig = new string[]
			{
				path
			};
			this.AddPathList(access, pathListOrig, false, true, false);
		}

		// Token: 0x060038EF RID: 14575 RVA: 0x000BFBB2 File Offset: 0x000BEBB2
		public FileIOPermission(FileIOPermissionAccess access, string[] pathList)
		{
			this.VerifyAccess(access);
			this.AddPathList(access, pathList, false, true, false);
		}

		// Token: 0x060038F0 RID: 14576 RVA: 0x000BFBCC File Offset: 0x000BEBCC
		public FileIOPermission(FileIOPermissionAccess access, AccessControlActions control, string path)
		{
			this.VerifyAccess(access);
			string[] pathListOrig = new string[]
			{
				path
			};
			this.AddPathList(access, control, pathListOrig, false, true, false);
		}

		// Token: 0x060038F1 RID: 14577 RVA: 0x000BFBFF File Offset: 0x000BEBFF
		public FileIOPermission(FileIOPermissionAccess access, AccessControlActions control, string[] pathList) : this(access, control, pathList, true, true)
		{
		}

		// Token: 0x060038F2 RID: 14578 RVA: 0x000BFC0C File Offset: 0x000BEC0C
		internal FileIOPermission(FileIOPermissionAccess access, string[] pathList, bool checkForDuplicates, bool needFullPath)
		{
			this.VerifyAccess(access);
			this.AddPathList(access, pathList, checkForDuplicates, needFullPath, true);
		}

		// Token: 0x060038F3 RID: 14579 RVA: 0x000BFC27 File Offset: 0x000BEC27
		internal FileIOPermission(FileIOPermissionAccess access, AccessControlActions control, string[] pathList, bool checkForDuplicates, bool needFullPath)
		{
			this.VerifyAccess(access);
			this.AddPathList(access, control, pathList, checkForDuplicates, needFullPath, true);
		}

		// Token: 0x060038F4 RID: 14580 RVA: 0x000BFC44 File Offset: 0x000BEC44
		public void SetPathList(FileIOPermissionAccess access, string path)
		{
			string[] pathList;
			if (path == null)
			{
				pathList = new string[0];
			}
			else
			{
				pathList = new string[]
				{
					path
				};
			}
			this.SetPathList(access, pathList, false);
		}

		// Token: 0x060038F5 RID: 14581 RVA: 0x000BFC73 File Offset: 0x000BEC73
		public void SetPathList(FileIOPermissionAccess access, string[] pathList)
		{
			this.SetPathList(access, pathList, true);
		}

		// Token: 0x060038F6 RID: 14582 RVA: 0x000BFC7E File Offset: 0x000BEC7E
		internal void SetPathList(FileIOPermissionAccess access, string[] pathList, bool checkForDuplicates)
		{
			this.SetPathList(access, AccessControlActions.None, pathList, checkForDuplicates);
		}

		// Token: 0x060038F7 RID: 14583 RVA: 0x000BFC8C File Offset: 0x000BEC8C
		internal void SetPathList(FileIOPermissionAccess access, AccessControlActions control, string[] pathList, bool checkForDuplicates)
		{
			this.VerifyAccess(access);
			if ((access & FileIOPermissionAccess.Read) != FileIOPermissionAccess.NoAccess)
			{
				this.m_read = null;
			}
			if ((access & FileIOPermissionAccess.Write) != FileIOPermissionAccess.NoAccess)
			{
				this.m_write = null;
			}
			if ((access & FileIOPermissionAccess.Append) != FileIOPermissionAccess.NoAccess)
			{
				this.m_append = null;
			}
			if ((access & FileIOPermissionAccess.PathDiscovery) != FileIOPermissionAccess.NoAccess)
			{
				this.m_pathDiscovery = null;
			}
			if ((control & AccessControlActions.View) != AccessControlActions.None)
			{
				this.m_viewAcl = null;
			}
			if ((control & AccessControlActions.Change) != AccessControlActions.None)
			{
				this.m_changeAcl = null;
			}
			this.m_unrestricted = false;
			this.AddPathList(access, control, pathList, checkForDuplicates, true, true);
		}

		// Token: 0x060038F8 RID: 14584 RVA: 0x000BFCFC File Offset: 0x000BECFC
		public void AddPathList(FileIOPermissionAccess access, string path)
		{
			string[] pathListOrig;
			if (path == null)
			{
				pathListOrig = new string[0];
			}
			else
			{
				pathListOrig = new string[]
				{
					path
				};
			}
			this.AddPathList(access, pathListOrig, false, true, false);
		}

		// Token: 0x060038F9 RID: 14585 RVA: 0x000BFD2D File Offset: 0x000BED2D
		public void AddPathList(FileIOPermissionAccess access, string[] pathList)
		{
			this.AddPathList(access, pathList, true, true, true);
		}

		// Token: 0x060038FA RID: 14586 RVA: 0x000BFD3A File Offset: 0x000BED3A
		internal void AddPathList(FileIOPermissionAccess access, string[] pathListOrig, bool checkForDuplicates, bool needFullPath, bool copyPathList)
		{
			this.AddPathList(access, AccessControlActions.None, pathListOrig, checkForDuplicates, needFullPath, copyPathList);
		}

		// Token: 0x060038FB RID: 14587 RVA: 0x000BFD4C File Offset: 0x000BED4C
		internal void AddPathList(FileIOPermissionAccess access, AccessControlActions control, string[] pathListOrig, bool checkForDuplicates, bool needFullPath, bool copyPathList)
		{
			this.VerifyAccess(access);
			if (pathListOrig == null)
			{
				throw new ArgumentNullException("pathList");
			}
			if (pathListOrig.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
			}
			if (this.m_unrestricted)
			{
				return;
			}
			string[] array = pathListOrig;
			if (copyPathList)
			{
				array = new string[pathListOrig.Length];
				Array.Copy(pathListOrig, array, pathListOrig.Length);
			}
			FileIOPermission.HasIllegalCharacters(array);
			ArrayList values = StringExpressionSet.CreateListFromExpressions(array, needFullPath);
			if ((access & FileIOPermissionAccess.Read) != FileIOPermissionAccess.NoAccess)
			{
				if (this.m_read == null)
				{
					this.m_read = new FileIOAccess();
				}
				this.m_read.AddExpressions(values, checkForDuplicates);
			}
			if ((access & FileIOPermissionAccess.Write) != FileIOPermissionAccess.NoAccess)
			{
				if (this.m_write == null)
				{
					this.m_write = new FileIOAccess();
				}
				this.m_write.AddExpressions(values, checkForDuplicates);
			}
			if ((access & FileIOPermissionAccess.Append) != FileIOPermissionAccess.NoAccess)
			{
				if (this.m_append == null)
				{
					this.m_append = new FileIOAccess();
				}
				this.m_append.AddExpressions(values, checkForDuplicates);
			}
			if ((access & FileIOPermissionAccess.PathDiscovery) != FileIOPermissionAccess.NoAccess)
			{
				if (this.m_pathDiscovery == null)
				{
					this.m_pathDiscovery = new FileIOAccess(true);
				}
				this.m_pathDiscovery.AddExpressions(values, checkForDuplicates);
			}
			if ((control & AccessControlActions.View) != AccessControlActions.None)
			{
				if (this.m_viewAcl == null)
				{
					this.m_viewAcl = new FileIOAccess();
				}
				this.m_viewAcl.AddExpressions(values, checkForDuplicates);
			}
			if ((control & AccessControlActions.Change) != AccessControlActions.None)
			{
				if (this.m_changeAcl == null)
				{
					this.m_changeAcl = new FileIOAccess();
				}
				this.m_changeAcl.AddExpressions(values, checkForDuplicates);
			}
		}

		// Token: 0x060038FC RID: 14588 RVA: 0x000BFE9C File Offset: 0x000BEE9C
		public string[] GetPathList(FileIOPermissionAccess access)
		{
			this.VerifyAccess(access);
			this.ExclusiveAccess(access);
			if (this.AccessIsSet(access, FileIOPermissionAccess.Read))
			{
				if (this.m_read == null)
				{
					return null;
				}
				return this.m_read.ToStringArray();
			}
			else if (this.AccessIsSet(access, FileIOPermissionAccess.Write))
			{
				if (this.m_write == null)
				{
					return null;
				}
				return this.m_write.ToStringArray();
			}
			else if (this.AccessIsSet(access, FileIOPermissionAccess.Append))
			{
				if (this.m_append == null)
				{
					return null;
				}
				return this.m_append.ToStringArray();
			}
			else
			{
				if (!this.AccessIsSet(access, FileIOPermissionAccess.PathDiscovery))
				{
					return null;
				}
				if (this.m_pathDiscovery == null)
				{
					return null;
				}
				return this.m_pathDiscovery.ToStringArray();
			}
		}

		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x060038FD RID: 14589 RVA: 0x000BFF38 File Offset: 0x000BEF38
		// (set) Token: 0x060038FE RID: 14590 RVA: 0x000BFFB8 File Offset: 0x000BEFB8
		public FileIOPermissionAccess AllLocalFiles
		{
			get
			{
				if (this.m_unrestricted)
				{
					return FileIOPermissionAccess.AllAccess;
				}
				FileIOPermissionAccess fileIOPermissionAccess = FileIOPermissionAccess.NoAccess;
				if (this.m_read != null && this.m_read.AllLocalFiles)
				{
					fileIOPermissionAccess |= FileIOPermissionAccess.Read;
				}
				if (this.m_write != null && this.m_write.AllLocalFiles)
				{
					fileIOPermissionAccess |= FileIOPermissionAccess.Write;
				}
				if (this.m_append != null && this.m_append.AllLocalFiles)
				{
					fileIOPermissionAccess |= FileIOPermissionAccess.Append;
				}
				if (this.m_pathDiscovery != null && this.m_pathDiscovery.AllLocalFiles)
				{
					fileIOPermissionAccess |= FileIOPermissionAccess.PathDiscovery;
				}
				return fileIOPermissionAccess;
			}
			set
			{
				if ((value & FileIOPermissionAccess.Read) != FileIOPermissionAccess.NoAccess)
				{
					if (this.m_read == null)
					{
						this.m_read = new FileIOAccess();
					}
					this.m_read.AllLocalFiles = true;
				}
				else if (this.m_read != null)
				{
					this.m_read.AllLocalFiles = false;
				}
				if ((value & FileIOPermissionAccess.Write) != FileIOPermissionAccess.NoAccess)
				{
					if (this.m_write == null)
					{
						this.m_write = new FileIOAccess();
					}
					this.m_write.AllLocalFiles = true;
				}
				else if (this.m_write != null)
				{
					this.m_write.AllLocalFiles = false;
				}
				if ((value & FileIOPermissionAccess.Append) != FileIOPermissionAccess.NoAccess)
				{
					if (this.m_append == null)
					{
						this.m_append = new FileIOAccess();
					}
					this.m_append.AllLocalFiles = true;
				}
				else if (this.m_append != null)
				{
					this.m_append.AllLocalFiles = false;
				}
				if ((value & FileIOPermissionAccess.PathDiscovery) != FileIOPermissionAccess.NoAccess)
				{
					if (this.m_pathDiscovery == null)
					{
						this.m_pathDiscovery = new FileIOAccess(true);
					}
					this.m_pathDiscovery.AllLocalFiles = true;
					return;
				}
				if (this.m_pathDiscovery != null)
				{
					this.m_pathDiscovery.AllLocalFiles = false;
				}
			}
		}

		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x060038FF RID: 14591 RVA: 0x000C00B0 File Offset: 0x000BF0B0
		// (set) Token: 0x06003900 RID: 14592 RVA: 0x000C0130 File Offset: 0x000BF130
		public FileIOPermissionAccess AllFiles
		{
			get
			{
				if (this.m_unrestricted)
				{
					return FileIOPermissionAccess.AllAccess;
				}
				FileIOPermissionAccess fileIOPermissionAccess = FileIOPermissionAccess.NoAccess;
				if (this.m_read != null && this.m_read.AllFiles)
				{
					fileIOPermissionAccess |= FileIOPermissionAccess.Read;
				}
				if (this.m_write != null && this.m_write.AllFiles)
				{
					fileIOPermissionAccess |= FileIOPermissionAccess.Write;
				}
				if (this.m_append != null && this.m_append.AllFiles)
				{
					fileIOPermissionAccess |= FileIOPermissionAccess.Append;
				}
				if (this.m_pathDiscovery != null && this.m_pathDiscovery.AllFiles)
				{
					fileIOPermissionAccess |= FileIOPermissionAccess.PathDiscovery;
				}
				return fileIOPermissionAccess;
			}
			set
			{
				if (value == FileIOPermissionAccess.AllAccess)
				{
					this.m_unrestricted = true;
					return;
				}
				if ((value & FileIOPermissionAccess.Read) != FileIOPermissionAccess.NoAccess)
				{
					if (this.m_read == null)
					{
						this.m_read = new FileIOAccess();
					}
					this.m_read.AllFiles = true;
				}
				else if (this.m_read != null)
				{
					this.m_read.AllFiles = false;
				}
				if ((value & FileIOPermissionAccess.Write) != FileIOPermissionAccess.NoAccess)
				{
					if (this.m_write == null)
					{
						this.m_write = new FileIOAccess();
					}
					this.m_write.AllFiles = true;
				}
				else if (this.m_write != null)
				{
					this.m_write.AllFiles = false;
				}
				if ((value & FileIOPermissionAccess.Append) != FileIOPermissionAccess.NoAccess)
				{
					if (this.m_append == null)
					{
						this.m_append = new FileIOAccess();
					}
					this.m_append.AllFiles = true;
				}
				else if (this.m_append != null)
				{
					this.m_append.AllFiles = false;
				}
				if ((value & FileIOPermissionAccess.PathDiscovery) != FileIOPermissionAccess.NoAccess)
				{
					if (this.m_pathDiscovery == null)
					{
						this.m_pathDiscovery = new FileIOAccess(true);
					}
					this.m_pathDiscovery.AllFiles = true;
					return;
				}
				if (this.m_pathDiscovery != null)
				{
					this.m_pathDiscovery.AllFiles = false;
				}
			}
		}

		// Token: 0x06003901 RID: 14593 RVA: 0x000C0234 File Offset: 0x000BF234
		private void VerifyAccess(FileIOPermissionAccess access)
		{
			if ((access & ~(FileIOPermissionAccess.Read | FileIOPermissionAccess.Write | FileIOPermissionAccess.Append | FileIOPermissionAccess.PathDiscovery)) != FileIOPermissionAccess.NoAccess)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Arg_EnumIllegalVal"), new object[]
				{
					(int)access
				}));
			}
		}

		// Token: 0x06003902 RID: 14594 RVA: 0x000C0272 File Offset: 0x000BF272
		private void ExclusiveAccess(FileIOPermissionAccess access)
		{
			if (access == FileIOPermissionAccess.NoAccess)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumNotSingleFlag"));
			}
			if ((access & access - 1) != FileIOPermissionAccess.NoAccess)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumNotSingleFlag"));
			}
		}

		// Token: 0x06003903 RID: 14595 RVA: 0x000C02A0 File Offset: 0x000BF2A0
		private static void HasIllegalCharacters(string[] str)
		{
			for (int i = 0; i < str.Length; i++)
			{
				if (str[i] == null)
				{
					throw new ArgumentNullException("str");
				}
				Path.CheckInvalidPathChars(str[i]);
				if (str[i].IndexOfAny(FileIOPermission.m_illegalCharacters) != -1)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPathChars"));
				}
			}
		}

		// Token: 0x06003904 RID: 14596 RVA: 0x000C02F3 File Offset: 0x000BF2F3
		private bool AccessIsSet(FileIOPermissionAccess access, FileIOPermissionAccess question)
		{
			return (access & question) != FileIOPermissionAccess.NoAccess;
		}

		// Token: 0x06003905 RID: 14597 RVA: 0x000C0300 File Offset: 0x000BF300
		private bool IsEmpty()
		{
			return !this.m_unrestricted && (this.m_read == null || this.m_read.IsEmpty()) && (this.m_write == null || this.m_write.IsEmpty()) && (this.m_append == null || this.m_append.IsEmpty()) && (this.m_pathDiscovery == null || this.m_pathDiscovery.IsEmpty()) && (this.m_viewAcl == null || this.m_viewAcl.IsEmpty()) && (this.m_changeAcl == null || this.m_changeAcl.IsEmpty());
		}

		// Token: 0x06003906 RID: 14598 RVA: 0x000C0395 File Offset: 0x000BF395
		public bool IsUnrestricted()
		{
			return this.m_unrestricted;
		}

		// Token: 0x06003907 RID: 14599 RVA: 0x000C03A0 File Offset: 0x000BF3A0
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return this.IsEmpty();
			}
			FileIOPermission fileIOPermission = target as FileIOPermission;
			if (fileIOPermission == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[]
				{
					base.GetType().FullName
				}));
			}
			return fileIOPermission.IsUnrestricted() || (!this.IsUnrestricted() && ((this.m_read == null || this.m_read.IsSubsetOf(fileIOPermission.m_read)) && (this.m_write == null || this.m_write.IsSubsetOf(fileIOPermission.m_write)) && (this.m_append == null || this.m_append.IsSubsetOf(fileIOPermission.m_append)) && (this.m_pathDiscovery == null || this.m_pathDiscovery.IsSubsetOf(fileIOPermission.m_pathDiscovery)) && (this.m_viewAcl == null || this.m_viewAcl.IsSubsetOf(fileIOPermission.m_viewAcl))) && (this.m_changeAcl == null || this.m_changeAcl.IsSubsetOf(fileIOPermission.m_changeAcl)));
		}

		// Token: 0x06003908 RID: 14600 RVA: 0x000C04A4 File Offset: 0x000BF4A4
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			FileIOPermission fileIOPermission = target as FileIOPermission;
			if (fileIOPermission == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[]
				{
					base.GetType().FullName
				}));
			}
			if (this.IsUnrestricted())
			{
				return target.Copy();
			}
			if (fileIOPermission.IsUnrestricted())
			{
				return this.Copy();
			}
			FileIOAccess fileIOAccess = (this.m_read == null) ? null : this.m_read.Intersect(fileIOPermission.m_read);
			FileIOAccess fileIOAccess2 = (this.m_write == null) ? null : this.m_write.Intersect(fileIOPermission.m_write);
			FileIOAccess fileIOAccess3 = (this.m_append == null) ? null : this.m_append.Intersect(fileIOPermission.m_append);
			FileIOAccess fileIOAccess4 = (this.m_pathDiscovery == null) ? null : this.m_pathDiscovery.Intersect(fileIOPermission.m_pathDiscovery);
			FileIOAccess fileIOAccess5 = (this.m_viewAcl == null) ? null : this.m_viewAcl.Intersect(fileIOPermission.m_viewAcl);
			FileIOAccess fileIOAccess6 = (this.m_changeAcl == null) ? null : this.m_changeAcl.Intersect(fileIOPermission.m_changeAcl);
			if ((fileIOAccess == null || fileIOAccess.IsEmpty()) && (fileIOAccess2 == null || fileIOAccess2.IsEmpty()) && (fileIOAccess3 == null || fileIOAccess3.IsEmpty()) && (fileIOAccess4 == null || fileIOAccess4.IsEmpty()) && (fileIOAccess5 == null || fileIOAccess5.IsEmpty()) && (fileIOAccess6 == null || fileIOAccess6.IsEmpty()))
			{
				return null;
			}
			return new FileIOPermission(PermissionState.None)
			{
				m_unrestricted = false,
				m_read = fileIOAccess,
				m_write = fileIOAccess2,
				m_append = fileIOAccess3,
				m_pathDiscovery = fileIOAccess4,
				m_viewAcl = fileIOAccess5,
				m_changeAcl = fileIOAccess6
			};
		}

		// Token: 0x06003909 RID: 14601 RVA: 0x000C0648 File Offset: 0x000BF648
		public override IPermission Union(IPermission other)
		{
			if (other == null)
			{
				return this.Copy();
			}
			FileIOPermission fileIOPermission = other as FileIOPermission;
			if (fileIOPermission == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[]
				{
					base.GetType().FullName
				}));
			}
			if (this.IsUnrestricted() || fileIOPermission.IsUnrestricted())
			{
				return new FileIOPermission(PermissionState.Unrestricted);
			}
			FileIOAccess fileIOAccess = (this.m_read == null) ? fileIOPermission.m_read : this.m_read.Union(fileIOPermission.m_read);
			FileIOAccess fileIOAccess2 = (this.m_write == null) ? fileIOPermission.m_write : this.m_write.Union(fileIOPermission.m_write);
			FileIOAccess fileIOAccess3 = (this.m_append == null) ? fileIOPermission.m_append : this.m_append.Union(fileIOPermission.m_append);
			FileIOAccess fileIOAccess4 = (this.m_pathDiscovery == null) ? fileIOPermission.m_pathDiscovery : this.m_pathDiscovery.Union(fileIOPermission.m_pathDiscovery);
			FileIOAccess fileIOAccess5 = (this.m_viewAcl == null) ? fileIOPermission.m_viewAcl : this.m_viewAcl.Union(fileIOPermission.m_viewAcl);
			FileIOAccess fileIOAccess6 = (this.m_changeAcl == null) ? fileIOPermission.m_changeAcl : this.m_changeAcl.Union(fileIOPermission.m_changeAcl);
			if ((fileIOAccess == null || fileIOAccess.IsEmpty()) && (fileIOAccess2 == null || fileIOAccess2.IsEmpty()) && (fileIOAccess3 == null || fileIOAccess3.IsEmpty()) && (fileIOAccess4 == null || fileIOAccess4.IsEmpty()) && (fileIOAccess5 == null || fileIOAccess5.IsEmpty()) && (fileIOAccess6 == null || fileIOAccess6.IsEmpty()))
			{
				return null;
			}
			return new FileIOPermission(PermissionState.None)
			{
				m_unrestricted = false,
				m_read = fileIOAccess,
				m_write = fileIOAccess2,
				m_append = fileIOAccess3,
				m_pathDiscovery = fileIOAccess4,
				m_viewAcl = fileIOAccess5,
				m_changeAcl = fileIOAccess6
			};
		}

		// Token: 0x0600390A RID: 14602 RVA: 0x000C0808 File Offset: 0x000BF808
		public override IPermission Copy()
		{
			FileIOPermission fileIOPermission = new FileIOPermission(PermissionState.None);
			if (this.m_unrestricted)
			{
				fileIOPermission.m_unrestricted = true;
			}
			else
			{
				fileIOPermission.m_unrestricted = false;
				if (this.m_read != null)
				{
					fileIOPermission.m_read = this.m_read.Copy();
				}
				if (this.m_write != null)
				{
					fileIOPermission.m_write = this.m_write.Copy();
				}
				if (this.m_append != null)
				{
					fileIOPermission.m_append = this.m_append.Copy();
				}
				if (this.m_pathDiscovery != null)
				{
					fileIOPermission.m_pathDiscovery = this.m_pathDiscovery.Copy();
				}
				if (this.m_viewAcl != null)
				{
					fileIOPermission.m_viewAcl = this.m_viewAcl.Copy();
				}
				if (this.m_changeAcl != null)
				{
					fileIOPermission.m_changeAcl = this.m_changeAcl.Copy();
				}
			}
			return fileIOPermission;
		}

		// Token: 0x0600390B RID: 14603 RVA: 0x000C08D0 File Offset: 0x000BF8D0
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = CodeAccessPermission.CreatePermissionElement(this, "System.Security.Permissions.FileIOPermission");
			if (!this.IsUnrestricted())
			{
				if (this.m_read != null && !this.m_read.IsEmpty())
				{
					securityElement.AddAttribute("Read", SecurityElement.Escape(this.m_read.ToString()));
				}
				if (this.m_write != null && !this.m_write.IsEmpty())
				{
					securityElement.AddAttribute("Write", SecurityElement.Escape(this.m_write.ToString()));
				}
				if (this.m_append != null && !this.m_append.IsEmpty())
				{
					securityElement.AddAttribute("Append", SecurityElement.Escape(this.m_append.ToString()));
				}
				if (this.m_pathDiscovery != null && !this.m_pathDiscovery.IsEmpty())
				{
					securityElement.AddAttribute("PathDiscovery", SecurityElement.Escape(this.m_pathDiscovery.ToString()));
				}
				if (this.m_viewAcl != null && !this.m_viewAcl.IsEmpty())
				{
					securityElement.AddAttribute("ViewAcl", SecurityElement.Escape(this.m_viewAcl.ToString()));
				}
				if (this.m_changeAcl != null && !this.m_changeAcl.IsEmpty())
				{
					securityElement.AddAttribute("ChangeAcl", SecurityElement.Escape(this.m_changeAcl.ToString()));
				}
			}
			else
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			return securityElement;
		}

		// Token: 0x0600390C RID: 14604 RVA: 0x000C0A28 File Offset: 0x000BFA28
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.ValidateElement(esd, this);
			if (XMLUtil.IsUnrestricted(esd))
			{
				this.m_unrestricted = true;
				return;
			}
			this.m_unrestricted = false;
			string text = esd.Attribute("Read");
			if (text != null)
			{
				this.m_read = new FileIOAccess(text);
			}
			else
			{
				this.m_read = null;
			}
			text = esd.Attribute("Write");
			if (text != null)
			{
				this.m_write = new FileIOAccess(text);
			}
			else
			{
				this.m_write = null;
			}
			text = esd.Attribute("Append");
			if (text != null)
			{
				this.m_append = new FileIOAccess(text);
			}
			else
			{
				this.m_append = null;
			}
			text = esd.Attribute("PathDiscovery");
			if (text != null)
			{
				this.m_pathDiscovery = new FileIOAccess(text);
				this.m_pathDiscovery.PathDiscovery = true;
			}
			else
			{
				this.m_pathDiscovery = null;
			}
			text = esd.Attribute("ViewAcl");
			if (text != null)
			{
				this.m_viewAcl = new FileIOAccess(text);
			}
			else
			{
				this.m_viewAcl = null;
			}
			text = esd.Attribute("ChangeAcl");
			if (text != null)
			{
				this.m_changeAcl = new FileIOAccess(text);
				return;
			}
			this.m_changeAcl = null;
		}

		// Token: 0x0600390D RID: 14605 RVA: 0x000C0B36 File Offset: 0x000BFB36
		int IBuiltInPermission.GetTokenIndex()
		{
			return FileIOPermission.GetTokenIndex();
		}

		// Token: 0x0600390E RID: 14606 RVA: 0x000C0B3D File Offset: 0x000BFB3D
		internal static int GetTokenIndex()
		{
			return 2;
		}

		// Token: 0x0600390F RID: 14607 RVA: 0x000C0B40 File Offset: 0x000BFB40
		[ComVisible(false)]
		public override bool Equals(object obj)
		{
			FileIOPermission fileIOPermission = obj as FileIOPermission;
			if (fileIOPermission == null)
			{
				return false;
			}
			if (this.m_unrestricted && fileIOPermission.m_unrestricted)
			{
				return true;
			}
			if (this.m_unrestricted != fileIOPermission.m_unrestricted)
			{
				return false;
			}
			if (this.m_read == null)
			{
				if (fileIOPermission.m_read != null && !fileIOPermission.m_read.IsEmpty())
				{
					return false;
				}
			}
			else if (!this.m_read.Equals(fileIOPermission.m_read))
			{
				return false;
			}
			if (this.m_write == null)
			{
				if (fileIOPermission.m_write != null && !fileIOPermission.m_write.IsEmpty())
				{
					return false;
				}
			}
			else if (!this.m_write.Equals(fileIOPermission.m_write))
			{
				return false;
			}
			if (this.m_append == null)
			{
				if (fileIOPermission.m_append != null && !fileIOPermission.m_append.IsEmpty())
				{
					return false;
				}
			}
			else if (!this.m_append.Equals(fileIOPermission.m_append))
			{
				return false;
			}
			if (this.m_pathDiscovery == null)
			{
				if (fileIOPermission.m_pathDiscovery != null && !fileIOPermission.m_pathDiscovery.IsEmpty())
				{
					return false;
				}
			}
			else if (!this.m_pathDiscovery.Equals(fileIOPermission.m_pathDiscovery))
			{
				return false;
			}
			if (this.m_viewAcl == null)
			{
				if (fileIOPermission.m_viewAcl != null && !fileIOPermission.m_viewAcl.IsEmpty())
				{
					return false;
				}
			}
			else if (!this.m_viewAcl.Equals(fileIOPermission.m_viewAcl))
			{
				return false;
			}
			if (this.m_changeAcl == null)
			{
				if (fileIOPermission.m_changeAcl != null && !fileIOPermission.m_changeAcl.IsEmpty())
				{
					return false;
				}
			}
			else if (!this.m_changeAcl.Equals(fileIOPermission.m_changeAcl))
			{
				return false;
			}
			return true;
		}

		// Token: 0x06003910 RID: 14608 RVA: 0x000C0CB4 File Offset: 0x000BFCB4
		[ComVisible(false)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04001D93 RID: 7571
		private FileIOAccess m_read;

		// Token: 0x04001D94 RID: 7572
		private FileIOAccess m_write;

		// Token: 0x04001D95 RID: 7573
		private FileIOAccess m_append;

		// Token: 0x04001D96 RID: 7574
		private FileIOAccess m_pathDiscovery;

		// Token: 0x04001D97 RID: 7575
		[OptionalField(VersionAdded = 2)]
		private FileIOAccess m_viewAcl;

		// Token: 0x04001D98 RID: 7576
		[OptionalField(VersionAdded = 2)]
		private FileIOAccess m_changeAcl;

		// Token: 0x04001D99 RID: 7577
		private bool m_unrestricted;

		// Token: 0x04001D9A RID: 7578
		private static readonly char[] m_illegalCharacters = new char[]
		{
			'?',
			'*'
		};
	}
}
