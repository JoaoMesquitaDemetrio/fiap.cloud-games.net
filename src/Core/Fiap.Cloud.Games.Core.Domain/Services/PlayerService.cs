using System.Security.Cryptography;
using Fiap.Cloud.Games.Core.Domain.Entities;
using Fiap.Cloud.Games.Core.Domain.Interfaces.Infra.Repository;
using Fiap.Cloud.Games.Core.Domain.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Cloud.Games.Core.Domain.Services;

public class PlayerService : BaseService, IPlayerService
{
    public PlayerService(IApplicationDBContext applicationDBContext) : base(applicationDBContext) { }

    public async Task<Player> GetById(Guid id, bool tracking = false)
    {
        var query = ById<Player>(id, tracking);
        return await query.FirstOrDefaultAsync();
    }

    public async Task Insert(Player player)
    {
        if (await Exists(player))
            throw new ArgumentException("Jogador já cadastrado com o mesmo nome ou e-mail. Tente a recuperação de senha.");

        player.PasswordHash = HashPassword(player.PasswordHash); 
        await _context.Set<Player>().AddAsync(player);
    }

    private async Task<bool> Exists(Player player)
    {
        var mail = player.Email.ToLower().Trim();
        var name = player.Name.ToLower().Trim();

        var query = BaseQuery<Player>(tracking: false)
            .Where(x => x.Email.ToLower().Trim() == mail || x.Name.ToLower().Trim() == name);

        var result = await query.AnyAsync();

        return result;
    }

    public string HashPassword(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(16);
        var hash = new Rfc2898DeriveBytes(password, salt, 100_000, HashAlgorithmName.SHA256);
        var hashBytes = hash.GetBytes(32);
        
        var hashWithSalt = new byte[48];
        Array.Copy(salt, 0, hashWithSalt, 0, 16);
        Array.Copy(hashBytes, 0, hashWithSalt, 16, 32);
        
        return Convert.ToBase64String(hashWithSalt);
    }
}
