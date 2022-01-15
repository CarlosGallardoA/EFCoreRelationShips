using EFCoreRelationShips.entities;
using EFCoreRelationShips.Dto;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreRelationShips.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly DataContext context;

        public CharacterController(DataContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Character>>> Get(int userId)
        {
            var characters = await this.context.Characters
                .Where(c => c.UserId == userId)
                .Include(c => c.Weapon)
                .Include(c => c.Skills)
                 .ToListAsync();
            return characters;
        }
        [HttpPost]
        public async Task<ActionResult<List<Character>>> Create(WaponDto request)
        {
            var user = await this.context.Users.FindAsync(request.UserId);
            if(user == null)
                return NotFound();

            var newCharacter = new Character
            {
                Username = request.Username,
                RpgClass = request.RpgClass,
                User = user,
            };
            this.context.Characters.AddAsync(newCharacter);
            await this.context.SaveChangesAsync();
            return await Get(newCharacter.UserId);
        }
        [HttpPost("weapon")]
        public async Task<ActionResult<Character>> AddWeapon(WeaponDto request)
        {
            var character = await this.context.Characters.FindAsync(request.CharaterId);
            if (character == null)
                return NotFound();

            var newWeapon = new Weapon
            {
                Name = request.Name,
                Damage = request.Damage,
                Character = character,
            };
            this.context.Weapons.AddAsync(newWeapon);
            await this.context.SaveChangesAsync();
            return character;
        }
        [HttpPost("skill")]
        public async Task<ActionResult<Character>> AddCharacterSkill(CharacterSkillDto request)
        {
            var character = await this.context.Characters.Where(c => c.Id == request.CharacterId)
                .Include(c => c.Skills)
                .FirstOrDefaultAsync();
            if (character == null)
                return NotFound();
            var skill = await this.context.Skills.FindAsync(request.SkillId);
            if (skill  == null)
                return NotFound();
            character.Skills.Add(skill);
            await this.context.SaveChangesAsync();
            return character;
        }
    }
}
