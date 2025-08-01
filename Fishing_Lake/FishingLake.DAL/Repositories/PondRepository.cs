﻿using FishingLake.DAL.Models;
using FishingLake.DAL;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using FishingLake.DAL.Repositories;

public class PondRepository:IPondRepository
{
    private FishingManagementContext _context;

    public List<Pond> GetAll()
    {
        _context = new FishingManagementContext();
        return _context.Pond
            .Include(p => p.Owner)
            .Include(p => p.PondFishes)
            .ThenInclude(pf => pf.Fish)
            .ToList();
    }

    public void Add(Pond pond)
    {
        _context = new FishingManagementContext();
        _context.Pond.Add(pond);
        _context.SaveChanges();
    }

    public void Update(Pond pond)
    {
        _context = new FishingManagementContext();
        _context.Pond.Update(pond);
        _context.SaveChanges();
    }

    

    public List<Pond> GetByOwnerId(int ownerId, bool includeHidden = false)
    {
        _context = new FishingManagementContext();
        var query = _context.Pond
            .Include(p => p.Owner)
            .Include(p => p.PondFishes)
            .ThenInclude(pf => pf.Fish)
            .Where(p => p.OwnerId == ownerId);

        if (!includeHidden)
            query = query.Where(p => !p.IsDeleted);

        return query.ToList();
    }


}