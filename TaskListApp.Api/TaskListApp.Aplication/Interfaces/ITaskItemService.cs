using TaskListApp.Aplication.Dtos.Request;
using TaskListApp.Aplication.Dtos.Response;
using TaskListApp.Domain.Entities;
using TaskListApp.Domain.Enums;

namespace TaskListApp.Aplication.Interfaces
{
    public interface ITaskItemService
    {
        Task<TaskItemResponseDto> GetByIdAsync(Guid id);
        Task<IEnumerable<TaskItemResponseDto>> GetAllAsync();
        Task<IEnumerable<TaskItemResponseDto>> GetAllByStatus(Status status);
        Task<Guid> AddAsync(TaskItemRequestDto taskItemRequestDto);
        Task<bool> UpdateAsync(Guid id, TaskItemRequestDto taskItemRequestDto);
        Task<bool> DeleteAsync(Guid id);
    }
}
