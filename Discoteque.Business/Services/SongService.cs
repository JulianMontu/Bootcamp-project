﻿using Discoteque.Business.IServices;
using Discoteque.Business.Utils;
using Discoteque.Data.Dto;
using Discoteque.Data.Models;
using Discoteque.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Discoteque.Business.Services
{
    public class SongService : ISongService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SongService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseMessage<Song>> CreateSong(Song newSong)
        {
            try
            {
                var album = await _unitOfWork.AlbumRepository.FindAsync(newSong.AlbumId);
                if (album == null)
                {
                    return Utilities.BuildResponse<Song>(HttpStatusCode.NotFound, BaseMessageStatus.ALBUM_NOT_FOUND);
                }
                await _unitOfWork.SongRepository.AddAsync(newSong);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                return Utilities.BuildResponse<Song>(HttpStatusCode.InternalServerError, ex.Message);
            }

            return Utilities.BuildResponse(HttpStatusCode.OK, BaseMessageStatus.OK_200, new List<Song>() { newSong });
        }

        public async Task<BaseMessage<Song>> CreateSongsInBatch(List<Song> songs)
        {
            try
            {
                foreach (var item in songs)
                {
                    var album = await _unitOfWork.AlbumRepository.FindAsync(item.AlbumId);
                    if (album != null)
                    {
                        await _unitOfWork.SongRepository.AddAsync(item);
                    }
                }

                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                return Utilities.BuildResponse<Song>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
            }
            return Utilities.BuildResponse(HttpStatusCode.OK, BaseMessageStatus.OK_200, songs);
        }

        public async Task<Song> GetById(int id)
        {
            return await _unitOfWork.SongRepository.FindAsync(id);
        }

        public async Task<IEnumerable<Song>> GetSongsAsync()
        {
            return await _unitOfWork.SongRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Song>> GetSongsByAlbum(int AlbumId)
        {
            return await _unitOfWork.SongRepository.GetAllAsync(x => x.AlbumId == AlbumId);
        }

        public async Task<IEnumerable<Song>> GetSongsByYear(int year)
        {
            return await _unitOfWork.SongRepository.GetAllAsync(x => x.Album.Year == year, includeProperties: new Album().GetType().Name);
        }

        public async Task<Song> UpdateSong(Song song)
        {
            await _unitOfWork.SongRepository.Update(song);
            await _unitOfWork.SaveAsync();
            return song;
        }

    }
}
