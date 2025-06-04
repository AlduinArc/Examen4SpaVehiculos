const apiServicios = "http://spavehiculosproy.runasp.net/api/servicios";
const servicioForm = document.getElementById("servicio-form");
const serviciosBody = document.getElementById("servicios-body");

let editando = null;

// Obtener y mostrar servicios
async function obtenerServicios() {
  try {
    const res = await fetch(`${apiServicios}/listar`);
    const data = await res.json();
    console.log("Servicios recibidos:", data); // Para depuración
    mostrarServicios(data);
  } catch (err) {
    console.error("Error al cargar los servicios", err);
  }
}

// Mostrar servicios en la tabla
function mostrarServicios(servicios) {
  serviciosBody.innerHTML = "";
  servicios.forEach(servicio => {
    const row = document.createElement("tr");
    row.innerHTML = `
      <td class="p-2 border">${servicio.idServicio}</td>
      <td class="p-2 border">${servicio.Nombre}</td>
      <td class="p-2 border">${servicio.Descripcion || ""}</td>
      <td class="p-2 border">${servicio.Precio}</td>
      <td class="p-2 border">${servicio.idSede}</td>
      <td class="p-2 border space-x-2">
        <button class="text-blue-600" onclick='editarServicio(${JSON.stringify(servicio).replace(/"/g, "&quot;")})'>Editar</button>
        <button class="text-red-600" onclick='eliminarServicio(${servicio.idServicio})'>Eliminar</button>
      </td>
    `;
    serviciosBody.appendChild(row);
  });
}

// Guardar servicio (crear o actualizar)
servicioForm.addEventListener("submit", async (e) => {
  e.preventDefault();

  const data = {
    Nombre: document.getElementById("nombre").value,
    Descripcion: document.getElementById("descripcion").value,
    Precio: parseFloat(document.getElementById("precio").value),
    IdSede: parseInt(document.getElementById("idSede").value),
  };

  if (editando) {
    data.IdServicio = editando.IdServicio;
    await fetch(`${apiServicios}/actualizar`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(data),
    });
    editando = null;
  } else {
    await fetch(`${apiServicios}/crear`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(data),
    });
  }

  servicioForm.reset();
  obtenerServicios();
});

// Cargar datos en el formulario
function editarServicio(servicio) {
  document.getElementById("nombre").value = servicio.Nombre;
  document.getElementById("descripcion").value = servicio.Descripcion;
  document.getElementById("precio").value = servicio.Precio;
  document.getElementById("idSede").value = servicio.IdSede;
  editando = servicio;
}

// Eliminar servicio
async function eliminarServicio(id) {
  if (confirm("¿Deseas eliminar este servicio?")) {
    await fetch(`${apiServicios}/eliminar?id=${id}`, { method: "DELETE" });
    obtenerServicios();
  }
}

// Inicializar
obtenerServicios();
