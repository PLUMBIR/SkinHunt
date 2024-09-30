using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SkinHunt.Application.Common.Entities;
using SkinHunt.Application.Common.Interfaces;
using SkinHunt.Application.Common.Models;

namespace SkinHunt.Application.Commands;

public class UpdateUserAvatarCommand : IRequest<UserDto>
{
    public UpdateUserAvatarModel Model { get; set; }
    
    public UpdateUserAvatarCommand(UpdateUserAvatarModel model)
    {
        Model = model;
    }
}

public class UpdateUserAvatarCommandHandler : IRequestHandler<UpdateUserAvatarCommand, UserDto>
{
    private readonly DbContext _dbContext;
    private readonly IJwtExtension _jwtExtension;
    private readonly IMapper _mapper;
    private readonly UserManager<UserEntity> _userManager;

    public UpdateUserAvatarCommandHandler(
        DbContext dbContext,
        IJwtExtension jwtExtension,
        IMapper mapper,
        UserManager<UserEntity> userManager)
    {
        _dbContext = dbContext;
        _jwtExtension = jwtExtension;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<UserDto> Handle(UpdateUserAvatarCommand request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.FirstAsync(o => o.Id == request.Model.UserId, cancellationToken);

        user.Avatar = request.Model.Avatar;

        await _dbContext.SaveChangesAsync(cancellationToken);
        
        var token = await _jwtExtension.GenerateTokenAsync(user);

        var userDto = await _dbContext.Users
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .FirstAsync(o => o.Id == request.Model.UserId);

        userDto.Token = token;

        var roles = await _userManager.GetRolesAsync(user);
        userDto.Roles = roles.ToArray();

        return userDto;
    }
}