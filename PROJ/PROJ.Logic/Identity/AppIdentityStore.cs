using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PROJ.Logic.Authorization;
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

        #region User Management
        public override IQueryable<AppIdentityUser> Users => _userManager.GetAll().Select(Mapper.Map<AppIdentityUser>).ToList().AsQueryable();

        public override Task<IdentityResult> CreateAsync(AppIdentityUser identityUser, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (identityUser == null)
            {
                throw new ArgumentNullException(nameof(identityUser));
            }

            var newUser = new UserDTO();
            Map(identityUser, newUser);

            newUser.Id = _userManager.Save(newUser);
            Mapper.Map(newUser, identityUser);

            var claim = new AppIdentityUserClaim(identityUser, RoleConstants.Role.User).ToClaim();
            AddClaimsAsync(identityUser, new[] { claim }, cancellationToken);

            var savedClaims = _userClaimManager.GetSpecificClaimByUserId(identityUser.Id, claim.Type, claim.Value);
            identityUser.UserClaims = Mapper.Map<IList<AppIdentityUserClaim>>(savedClaims);

            UpdateAsync(identityUser, cancellationToken);

            return Task.FromResult(IdentityResult.Success);
        }

        public override Task<IdentityResult> DeleteAsync(AppIdentityUser identityUser, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (identityUser == null)
            {
                throw new ArgumentNullException(nameof(identityUser));
            }

            _userManager.Delete(identityUser.Id);

            return Task.FromResult(IdentityResult.Success);
        }

        public override Task<AppIdentityUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            var user = _userManager.Get(int.Parse(userId));
            return Task.FromResult(Mapper.Map<AppIdentityUser>(user));
        }

        public override Task<AppIdentityUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            var user = _userManager.FindByName(normalizedUserName);
            return Task.FromResult(Mapper.Map<AppIdentityUser>(user));
        }

        public override Task<AppIdentityUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        protected override Task<AppIdentityUser> FindUserAsync(int userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            var user = _userManager.Get(userId);
            return Task.FromResult(Mapper.Map<AppIdentityUser>(user));
        }

        public override Task<string> GetNormalizedUserNameAsync(AppIdentityUser identityUser, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (identityUser == null)
            {
                throw new ArgumentNullException(nameof(identityUser));
            }

            return Task.FromResult(identityUser.NormalizedUserName);
        }

        public override Task<string> GetUserIdAsync(AppIdentityUser identityUser, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (identityUser == null)
            {
                throw new ArgumentNullException(nameof(identityUser));
            }

            return Task.FromResult(identityUser.Id.ToString());
        }

        public override Task<string> GetUserNameAsync(AppIdentityUser identityUser, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (identityUser == null)
            {
                throw new ArgumentNullException(nameof(identityUser));
            }

            return Task.FromResult(identityUser.UserName);
        }

        public override Task SetNormalizedUserNameAsync(AppIdentityUser identityUser, string normalizedName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (identityUser == null)
            {
                throw new ArgumentNullException(nameof(identityUser));
            }

            return Task.FromResult(identityUser.NormalizedUserName = normalizedName);
        }

        public override Task SetUserNameAsync(AppIdentityUser identityUser, string userName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (identityUser == null)
            {
                throw new ArgumentNullException(nameof(identityUser));
            }

            return Task.FromResult(identityUser.UserName = userName);
        }

        public override Task<IdentityResult> UpdateAsync(AppIdentityUser identityUser, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (identityUser == null)
            {
                throw new ArgumentNullException(nameof(identityUser));
            }

            var user = new UserDTO();
            Map(identityUser, user);

            _userManager.Save(user);

            return Task.FromResult(IdentityResult.Success);
        }
        #endregion

        #region Password Management
        public override Task<string> GetPasswordHashAsync(AppIdentityUser identityUser, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (identityUser == null)
            {
                throw new ArgumentNullException(nameof(identityUser));
            }

            return Task.FromResult(identityUser.PasswordHash);
        }

        public override Task<bool> HasPasswordAsync(AppIdentityUser identityUser, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (identityUser == null)
            {
                throw new ArgumentNullException(nameof(identityUser));
            }

            return Task.FromResult(!string.IsNullOrEmpty(identityUser.PasswordHash) && string.IsNullOrWhiteSpace(identityUser.PasswordHash) && identityUser.PasswordHash.Length > 0);
        }

        public override Task SetPasswordHashAsync(AppIdentityUser identityUser, string passwordHash, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (identityUser == null)
            {
                throw new ArgumentNullException(nameof(identityUser));
            }

            return Task.FromResult(identityUser.PasswordHash = passwordHash);
        }
        #endregion

        #region Claim Management
        public override Task<IList<Claim>> GetClaimsAsync(AppIdentityUser identityUser, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (identityUser == null)
            {
                throw new ArgumentNullException(nameof(identityUser));
            }

            var claims = _userClaimManager.GetByUserId(identityUser.Id)
                .Select(Mapper.Map<AppIdentityUserClaim>)
                .Select(x => x.ToClaim())
                .ToList() as IList<Claim>;

           return Task.FromResult(claims);
        }

        public override Task AddClaimsAsync(AppIdentityUser identityUser, IEnumerable<Claim> claims, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (identityUser == null)
            {
                throw new ArgumentNullException(nameof(identityUser));
            }
            if (claims == null)
            {
                throw new ArgumentNullException(nameof(claims));
            }

            var user = _userManager.Get(identityUser.Id);
            if (user == null)
            {
                return Task.FromResult(IdentityResult.Failed(
                    new IdentityError { Code = "UserNotFound", Description = $"User with id {user.Id} not found" }));
            }

            foreach (var claim in claims)
            {
                var newClaim = CreateUserClaim(identityUser, claim);
                var userClaim = Mapper.Map<UserClaimDTO>(newClaim);
                userClaim.Id = 0;
                userClaim.User = user;
                _userClaimManager.Save(userClaim);
            }

            return Task.FromResult(IdentityResult.Success);
        }

        public override Task ReplaceClaimAsync(AppIdentityUser identityUser, Claim claim, Claim newClaim, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (identityUser == null)
            {
                throw new ArgumentNullException(nameof(identityUser));
            }
            if (claim == null)
            {
                throw new ArgumentNullException(nameof(claim));
            }
            if (newClaim == null)
            {
                throw new ArgumentNullException(nameof(newClaim));
            }

            var matchedClaims = _userClaimManager.GetSpecificClaimByUserId(identityUser.Id, claim.Type, claim.Value);

            foreach (var matchedClaim in matchedClaims)
            {
                matchedClaim.ClaimType = newClaim.Type;
                matchedClaim.ClaimValue = newClaim.Value;
                _userClaimManager.Save(matchedClaim);
            }

            return Task.FromResult(IdentityResult.Success);
        }

        public override Task RemoveClaimsAsync(AppIdentityUser identityUser, IEnumerable<Claim> claims, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (identityUser == null)
            {
                throw new ArgumentNullException(nameof(identityUser));
            }
            if (claims == null)
            {
                throw new ArgumentNullException(nameof(claims));
            }

            foreach (var claim in claims)
            {
                var userClaims = _userClaimManager.GetSpecificClaimByUserId(identityUser.Id, claim.Type, claim.Value);
                foreach (var userClaim in userClaims)
                {
                    _userClaimManager.Delete(userClaim.Id);
                }
            }

            return Task.FromResult(IdentityResult.Success);
        }

        public override Task<IList<AppIdentityUser>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (claim == null)
            {
                throw new ArgumentNullException(nameof(claim));
            }

            var users = _userManager.GetByClaim(claim.Type, claim.Value);
            return Task.FromResult(Mapper.Map<IList<AppIdentityUser>>(users));
        }
        #endregion

        #region External Login Management
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
        #endregion

        #region Token Management
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
        #endregion
    }
}
