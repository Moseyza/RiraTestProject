using Grpc.Core;
using RiragRPCTestProject.Protos;
using RiragRPCTestProject.Data;
using RiragRPCTestProject.Models;
using Microsoft.EntityFrameworkCore;
using Google.Protobuf.WellKnownTypes;

namespace RiragRPCTestProject.Services
{
    public class PersonServiceImpl : PersonService.PersonServiceBase

    {
        private readonly AppDbContext _context;

        public PersonServiceImpl(AppDbContext context)
        {
            _context = context;
        }


        public override async Task<PersonResponse> CreatePerson(Person request, ServerCallContext context)
        {
            try
            {
                var person = new PersonEntity()
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    NationalCode = request.NationalCode,
                    BirthDate = request.BirthDate.ToDateTime()
                };
                _context.People.Add(person);
                await _context.SaveChangesAsync();
                return new PersonResponse { Message = "Person created successfully", Success = true };
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Internal, $"Unexpected error: {ex.Message}"));
            }
        }


        public override async Task<Person> GetPerson(PersonIdRequest request, ServerCallContext context)
        {
            try
            {
                var person = await _context.People.FindAsync(request.Id);
                if (person == null)
                    throw new RpcException(new Status(StatusCode.NotFound, "Person not found"));
                return new Person { Id = person.Id, FirstName = person.FirstName, LastName = person.LastName, NationalCode = person.NationalCode, BirthDate = Timestamp.FromDateTime(person.BirthDate) };
            }
            catch (RpcException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Internal, $"Unexpected error: {ex.Message}"));
            }
        }

        public override async Task<PersonList> GetAllPeople(Protos.Empty request, ServerCallContext context)
        {
            try
            {
                var users = await _context.People.ToListAsync();
                var response = new PersonList();
                response.People.AddRange(users.Select(u => new Person { Id = u.Id, FirstName = u.FirstName, LastName = u.LastName, NationalCode = u.NationalCode, BirthDate = Timestamp.FromDateTime(u.BirthDate) }));
                return response;
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Internal, $"Unexpected error: {ex.Message}"));
            }
        }

        public override async Task<PersonResponse> UpdatePerson(Person request, ServerCallContext context)
        {
            try
            {
                var person = await _context.People.FindAsync(request.Id);
                if (person == null) 
                    throw new RpcException(new Status(StatusCode.NotFound, "Person not found"));
                person.FirstName = request.FirstName;
                person.LastName = request.LastName;
                person.NationalCode = request.NationalCode;
                person.BirthDate = request.BirthDate.ToDateTime();

                await _context.SaveChangesAsync();
                return new PersonResponse { Message = "Person updated successfully", Success = true };
            }

            catch (RpcException) 
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Internal, $"Unexpected error: {ex.Message}"));
            }

        }

        public override async Task<PersonResponse> DeletePerson(PersonIdRequest request, ServerCallContext context)
        {
            try
            {
                var person = await _context.People.FindAsync(request.Id);
                if (person == null) return new PersonResponse { Message = "Person not found", Success = false };

                _context.People.Remove(person);
                await _context.SaveChangesAsync();

                return new PersonResponse { Message = "Person deleted successfully", Success = true };
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Internal, $"Unexpected error: {ex.Message}"));
            }
        }
    }
}