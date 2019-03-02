using AutoMapper;
using Microsoft.AspNetCore.Identity;
using NHibernate;
using PROJ.DataAccess.Entities;
using PROJ.Logic.DTOs;
using PROJ.Logic.Managers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace PROJ.Logic.Identity
{
    public class AppIdentityStore : UserStoreBase<AppIdentityUser, int, AppIdentityUserClaim, IdentityUserLogin<int>, IdentityUserToken<int>>
    {
        private readonly IUserManager _userManager;
        private readonly IUserClaimManager _userClaimManager;

        public AppIdentityStore(AppIdentityErrorDescriber describer, IUserManager userManager, IUserClaimManager userClaimManager) : base(describer)
        {
            _userManager = userManager;
            _userClaimManager = userClaimManager;
        }

        private void Map(AppIdentityUser identityUser, UserDTO user)
        {
            Mapper.Map(identityUser, user);
        }

        public override IQueryable<AppIdentityUser> Users => _userManager.GetAll().Select(Mapper.Map<AppIdentityUser>).ToList().AsQueryable();

        #region UserStore
        public override Task<IdentityResult> CreateAsync(AppIdentityUser identityUser, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (identityUser == null)
            {
                throw new ArgumentNullException(nameof(identityUser));
            }

            var newUser = new UserDTO();
            Map(identityUser, newUser);

            _userManager.Save(newUser);

            Mapper.Map(newUser, identityUser);

            return Task.FromResult(IdentityResult.Success);
        }

        public override async Task<IdentityResult> DeleteAsync(AppIdentityUser identityUser, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            //try
            //{
            //    await _session.DeleteAsync(identityUser);
            //}
            //catch (Exception ex)
            //{
            //    // TODO error handling
            //    return IdentityResult.Failed(new IdentityError
            //    {
            //        Code = "0",
            //        Description = ex.Message
            //    });
            //}

            return IdentityResult.Success;
        }

        public override async Task<AppIdentityUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            //cancellationToken.ThrowIfCancellationRequested();
            //var identityUser = await _session.GetAsync<AppIdentityUser>(userId);
            //return identityUser;
            throw new NotImplementedException();
        }

        public override Task<AppIdentityUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var user = _userManager.GetAll().SingleOrDefault(x => x.NormalizedUserName == normalizedUserName);
            var entity = Mapper.Map<User>(user);
            return Task.FromResult(Mapper.Map<AppIdentityUser>(entity));
        }

        public  override async Task<string> GetNormalizedUserNameAsync(AppIdentityUser identityUser, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await Task.Run(() => identityUser.NormalizedUserName);
        }

        public  override async Task<string> GetUserIdAsync(AppIdentityUser identityUser, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await Task.Run(() => identityUser.Id.ToString());
        }

        public  override async Task<string> GetUserNameAsync(AppIdentityUser identityUser, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await Task.Run(() => identityUser.UserName);
        }

        public  override async Task SetNormalizedUserNameAsync(AppIdentityUser identityUser, string normalizedName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await Task.Run(() => identityUser.NormalizedUserName = normalizedName);
        }

        public  override async Task SetUserNameAsync(AppIdentityUser identityUser, string userName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await Task.Run(() => identityUser.UserName = userName);
        }

        public  override async Task<IdentityResult> UpdateAsync(AppIdentityUser identityUser, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            //try
            //{
            //    await _session.SaveOrUpdateAsync(identityUser);
            //}
            //catch (Exception ex)
            //{
            //    return IdentityResult.Failed(new IdentityError
            //    {
            //        // TODO error handling
            //        Code = "0",
            //        Description = ex.Message
            //    });
            //}

            //return IdentityResult.Success;
            throw new NotImplementedException();

        }
        #endregion

        public  override async Task<string> GetPasswordHashAsync(AppIdentityUser identityUser, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await Task.Run(() => identityUser.PasswordHash);
        }

        public  override async Task<bool> HasPasswordAsync(AppIdentityUser identityUser, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await Task.Run(() => !string.IsNullOrEmpty(identityUser.PasswordHash) && string.IsNullOrWhiteSpace(identityUser.PasswordHash) && identityUser.PasswordHash.Length > 0);
        }

        public  override async Task SetPasswordHashAsync(AppIdentityUser identityUser, string passwordHash, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await Task.Run(() => identityUser.PasswordHash = passwordHash);
        }

        protected override Task<AppIdentityUser> FindUserAsync(int userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public override Task<IList<Claim>> GetClaimsAsync(AppIdentityUser identityUser, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (identityUser == null)
            {
                throw new ArgumentNullException(nameof(identityUser));
            }

            var claims = _userClaimManager
                .GetAll()
                .Where(x => x.User.Id == identityUser.Id)
                .Select(Mapper.Map<AppIdentityUserClaim>)
                .Select(x => x.ToClaim())
                .ToList() as IList<Claim>;

            return Task.FromResult(claims);
        }

        public override Task AddClaimsAsync(AppIdentityUser identityUser, IEnumerable<Claim> claims, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public override Task ReplaceClaimAsync(AppIdentityUser identityUser, Claim claim, Claim newClaim, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public override Task RemoveClaimsAsync(AppIdentityUser identityUser, IEnumerable<Claim> claims, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public override Task<IList<AppIdentityUser>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        protected override Task<IdentityUserLogin<int>> FindUserLoginAsync(int userId, string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        protected override Task<IdentityUserLogin<int>> FindUserLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public override Task AddLoginAsync(AppIdentityUser user, UserLoginInfo login, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public override Task RemoveLoginAsync(AppIdentityUser user, string loginProvider, string providerKey, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public override Task<IList<UserLoginInfo>> GetLoginsAsync(AppIdentityUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public override Task<AppIdentityUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        protected override Task<IdentityUserToken<int>> FindTokenAsync(AppIdentityUser user, string loginProvider, string name, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        protected override Task AddUserTokenAsync(IdentityUserToken<int> token)
        {
            throw new NotImplementedException();
        }

        protected override Task RemoveUserTokenAsync(IdentityUserToken<int> token)
        {
            throw new NotImplementedException();
        }
    }
}
