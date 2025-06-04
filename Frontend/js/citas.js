const apiCitas = "http://spavehiculosproy.runasp.net/api/citas";
const apiClientes = "http://spavehiculosproy.runasp.net/api/clientes/listar";

const citaForm = document.getElementById("cita-form");
const citasBody = document.getElementById("citas-body");
const selectCliente = document.getElementById("idCliente");
const idCitaInput = document.getElementById("idCita");

let clientesMap = {};

// Cargar clientes al inicio
async function cargarClientes() {
  const res = await fetch(apiClientes);
  const clientes = await res.json();

  clientes.forEach(cliente => {
    const nombreCompleto = `${cliente.Nombre} ${cliente.Apellido}`;
    clientesMap[cliente.idCliente] = nombreCompleto;

    const option = document.createElement("option");
    option.value = cliente.idCliente;
    option.textContent = nombreCompleto;
    selectCliente.appendChild(option);
  });
}

// Obtener y mostrar citas
async function obtenerCitas() {
  try {
    const res = await fetch(`${apiCitas}/listar`);
    const data = await res.json();
    mostrarCitas(data);
  } catch (err) {
    console.error("Error cargando citas", err);
  }
}

// Mostrar citas en tabla
function mostrarCitas(citas) {
  citasBody.innerHTML = "";
  citas.forEach(cita => {
    const clienteNombre = clientesMap[cita.idCliente] || `ID ${cita.idCliente}`;
    const tr = document.createElement("tr");
    tr.innerHTML = `
      <td class="p-2 border">${cita.idCita}</td>
      <td class="p-2 border">${clienteNombre}</td>
      <td class="p-2 border">${formatearFechaCorta(cita.Fecha)}</td>
      <td class="p-2 border">${cita.Hora}</td>
      <td class="p-2 border">
        <button class="text-blue-600" onclick='editarCita(${JSON.stringify(cita).replace(/"/g, "&quot;")})'>Editar</button>
        <button class="text-red-600" onclick="eliminarCita(${cita.idCita})">Eliminar</button>
      </td>
    `;
    citasBody.appendChild(tr);
  });
}

// Función para formatear la Fecha como dd/mm/aaaa
function formatearFechaCorta(FechaISO) {
  if (!FechaISO) return "Sin Fecha";
  const Fecha = new Date(FechaISO);
  return Fecha.toLocaleDateString("es-ES", {
    year: "numeric",
    month: "2-digit",
    day: "2-digit",
  });
}

// Guardar cita
citaForm.addEventListener("submit", async (e) => {
  e.preventDefault();

  const idCita = idCitaInput.value.trim();

  const data = {
    idCliente: parseInt(selectCliente.value),
    Fecha: document.getElementById("fecha").value,
    Hora: document.getElementById("hora").value,
  };

  try {
    if (idCita) {
      data.idCita = parseInt(idCita);
      await fetch(`${apiCitas}/actualizar`, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(data),
      });
    } else {
      await fetch(`${apiCitas}/crear`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(data),
      });
    }

    citaForm.reset();
    idCitaInput.value = ""; // limpiar campo oculto
    obtenerCitas();
  } catch (error) {
    console.error("Error al guardar la cita:", error);
  }
});

// Editar cita
function editarCita(cita) {
  selectCliente.value = cita.idCliente;
  document.getElementById("fecha").value = cita.Fecha;
  document.getElementById("hora").value = cita.Hora;
  idCitaInput.value = cita.idCita;
}

// Eliminar cita
async function eliminarCita(id) {
  if (confirm("¿Estás seguro de eliminar esta cita?")) {
    await fetch(`${apiCitas}/eliminar?id=${id}`, { method: "DELETE" });
    obtenerCitas();
  }
}

// Inicializar
(async function iniciar() {
  await cargarClientes();
  await obtenerCitas();
})();
