using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MasterDataManagement.Client.Data;
using MasterDataManagement.Client.Models;

namespace MasterDataManagement.Client.Controllers
{
    public class SysOwnerDtoesController : Controller
    {
        private readonly MasterDataManagementClientContext _context;

        public SysOwnerDtoesController(MasterDataManagementClientContext context)
        {
            _context = context;
        }

        // GET: SysOwnerDtoes
        public async Task<IActionResult> Index()
        {
              return _context.SysOwnerDto != null ? 
                          View(await _context.SysOwnerDto.ToListAsync()) :
                          Problem("Entity set 'MasterDataManagementClientContext.SysOwnerDto'  is null.");
        }

        // GET: SysOwnerDtoes/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.SysOwnerDto == null)
            {
                return NotFound();
            }

            var sysOwnerDto = await _context.SysOwnerDto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sysOwnerDto == null)
            {
                return NotFound();
            }

            return View(sysOwnerDto);
        }

        // GET: SysOwnerDtoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SysOwnerDtoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OwnerName,OwnerCode,Description,ParentOwnerId")] SysOwnerDto sysOwnerDto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sysOwnerDto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sysOwnerDto);
        }

        // GET: SysOwnerDtoes/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.SysOwnerDto == null)
            {
                return NotFound();
            }

            var sysOwnerDto = await _context.SysOwnerDto.FindAsync(id);
            if (sysOwnerDto == null)
            {
                return NotFound();
            }
            return View(sysOwnerDto);
        }

        // POST: SysOwnerDtoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,OwnerName,OwnerCode,Description,ParentOwnerId")] SysOwnerDto sysOwnerDto)
        {
            if (id != sysOwnerDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sysOwnerDto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SysOwnerDtoExists(sysOwnerDto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(sysOwnerDto);
        }

        // GET: SysOwnerDtoes/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.SysOwnerDto == null)
            {
                return NotFound();
            }

            var sysOwnerDto = await _context.SysOwnerDto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sysOwnerDto == null)
            {
                return NotFound();
            }

            return View(sysOwnerDto);
        }

        // POST: SysOwnerDtoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.SysOwnerDto == null)
            {
                return Problem("Entity set 'MasterDataManagementClientContext.SysOwnerDto'  is null.");
            }
            var sysOwnerDto = await _context.SysOwnerDto.FindAsync(id);
            if (sysOwnerDto != null)
            {
                _context.SysOwnerDto.Remove(sysOwnerDto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SysOwnerDtoExists(long id)
        {
          return (_context.SysOwnerDto?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
