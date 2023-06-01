﻿using DataLayer;
using DataLayer.Dtos;
using DataLayer.Entities;

namespace Core.Services;
public class CommunityCollectionService : ICommunityCollectionService {
	private readonly UnitOfWork _unitOfWork;

	public CommunityCollectionService(UnitOfWork unitOfWork) {
		_unitOfWork = unitOfWork;
	}

	public void Add(Community entity) {
		_unitOfWork.CommunityRepository.Add(entity);
		_unitOfWork.SaveChanges();
	}

	public void AddCommunityDto(CommunityDto communityDto) {
		var community = new Community {
			Name = communityDto.Name,
			Description = communityDto.Description,
			ModeratorId = communityDto.ModeratorId,
			Moderator = communityDto.Moderator.ToUser(),
		};

		Add(community);
	}

	public void Delete(int id) {
		var community = _unitOfWork.CommunityRepository.GetById(id) ?? throw new Exception("Community not found");

		_unitOfWork.CommunityRepository.Remove(community);
		_unitOfWork.SaveChanges();
	}

	public void DeleteCommunityDto(int id) {
		var community = GetById(id) ?? throw new Exception("Community not found");

		Delete(community.Id);
	}

	public List<Community> GetAll() {
		var results = _unitOfWork.CommunityRepository.GetAll();

		return results;
	}

	public Community? GetById(int id) {
		return _unitOfWork.CommunityRepository.GetById(id);
	}

	public CommunityDto? GetCommunityDtoById(int id) {
		var communityDto = GetById(id)?.ToCommunityDto();

		return communityDto;
	}

	public List<CommunityDto>? GetCommunityDtos() {
		var communityDtos = GetAll().ToCommunityDtos();

		return communityDtos;
	}

	public void Update(Community entity) {  // TODO: check if its good
		var community = _unitOfWork.CommunityRepository.GetById(entity.Id) ?? throw new Exception("Community not found");

		community.Name = entity.Name;
		community.Description = entity.Description;
		community.ModeratorId = entity.ModeratorId;
		community.Moderator = entity.Moderator;

		_unitOfWork.CommunityRepository.Update(entity);
		_unitOfWork.SaveChanges();
	}

	public void UpdateCommunityDto(CommunityDto communityDto) {
		var community = GetById(communityDto.Id) ?? throw new Exception("Community not found");

		community.Name = communityDto.Name;
		community.Description = communityDto.Description;
		community.ModeratorId = communityDto.ModeratorId;
		community.Moderator = communityDto.Moderator.ToUser();

		Update(community);
	}
}