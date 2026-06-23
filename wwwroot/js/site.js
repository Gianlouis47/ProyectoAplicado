
function showModal(id) {
    const modal = document.getElementById(id);
    if (modal) modal.classList.add('show');
}

function hideModal(id) {
    const modal = document.getElementById(id);
    if (modal) modal.classList.remove('show');
}


document.addEventListener('click', function(e) {
    if (e.target.classList.contains('modal-overlay')) {
        e.target.classList.remove('show');
    }
});

document.addEventListener('DOMContentLoaded', function() {
    const path = window.location.pathname.toLowerCase();
    document.querySelectorAll('.nav-item').forEach(item => {
        const href = item.getAttribute('href')?.toLowerCase() || '';
        if (href && path.startsWith(href) && href !== '/') {
            item.classList.add('active');
        }
    });
});


function filtrarTabla(inputId, tablaId) {
    const filtro = document.getElementById(inputId).value.toLowerCase();
    const filas = document.querySelectorAll('#' + tablaId + ' tbody tr');
    
    filas.forEach(fila => {
        const texto = fila.textContent.toLowerCase();
        fila.style.display = texto.includes(filtro) ? '' : 'none';
    });
}

function confirmarEliminar(nombreProducto, id) {
    document.getElementById('msgEliminar').innerHTML = 
        '¿Estás seguro de eliminar <strong style="color:var(--gold)">' + nombreProducto + '</strong>?<br>' +
        '<span style="font-size:12px;color:var(--text-muted)">Esta acción no se puede deshacer.</span>';
    document.getElementById('btnConfirmarEliminar').setAttribute('data-id', id);
    showModal('modalEliminar');
}

function ejecutarEliminar() {
    const id = document.getElementById('btnConfirmarEliminar').getAttribute('data-id');
    const btn = document.getElementById('btnConfirmarEliminar');
    
    btn.disabled = true;
    btn.textContent = 'Eliminando...';
    hideModal('modalEliminar');

    
    setTimeout(function() {
        const fila = document.querySelector('tr[data-id="' + id + '"]');
        if (fila) fila.style.display = 'none';
        btn.disabled = false;
        btn.textContent = 'Sí, eliminar';
        showModal('modalEliminadoOk');
    }, 800);
}

function actualizarContadores() {
   
    const filas = document.querySelectorAll('.table-products tbody tr');
    let total = 0, bajos = 0, vacios = 0;
    
    filas.forEach(fila => {
        if (fila.style.display !== 'none') {
            total++;
            if (fila.querySelector('.badge-bajo')) bajos++;
            if (fila.querySelector('.badge-vacio')) vacios++;
        }
    });
    
    const elTotal = document.getElementById('countTotal');
    const elBajos = document.getElementById('countBajos');
    const elVacios = document.getElementById('countVacios');
    
    if (elTotal) elTotal.textContent = total;
    if (elBajos) elBajos.textContent = bajos;
    if (elVacios) elVacios.textContent = vacios;
}

function filtrarPorEstado(valor) {
    const filas = document.querySelectorAll('.table-products tbody tr');
    filas.forEach(fila => {
        if (valor === 'todos') {
            fila.style.display = '';
        } else {
            const badge = fila.querySelector('.badge');
            if (badge) {
                if (valor === 'ok' && badge.classList.contains('badge-ok')) fila.style.display = '';
                else if (valor === 'bajo' && badge.classList.contains('badge-bajo')) fila.style.display = '';
                else if (valor === 'vacio' && badge.classList.contains('badge-vacio')) fila.style.display = '';
                else fila.style.display = 'none';
            } else {
                fila.style.display = 'none';
            }
        }
    });
    actualizarContadores();
}