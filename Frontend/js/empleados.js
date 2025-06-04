const form = document.getElementById("empleado-form");
const tbody = document.getElementById("empleados-body");
const apiURL = "http://spavehiculosproy.runasp.net/api/empleados";

async function cargarEmpleados() {
  const res = await fetch(apiURL + "/listar");
  const empleados = await res.json();
  tbody.innerHTML = "";
  empleados.forEach(e => {
    const tr = document.createElement("tr");
    tr.innerHTML = `
      <td class="p-2 border">${e.IdEmpleado || e.idEmpleado}</td>
      <td class="p-2 border">${e.Nombre || e.nombre}</td>
      <td class="p-2 border">${e.Apellido || e.apellido}</td>
      <td class="p-2 border">${e.IdServicio || e.idServicio}</td>
      <td class="p-2 border">
        <button onclick="editar(${e.IdEmpleado || e.idEmpleado})" class="text-blue-500">Editar</button>
        <button onclick="eliminar(${e.IdEmpleado || e.idEmpleado})" class="text-red-500 ml-2">Eliminar</button>
      </td>
    `;
    tbody.appendChild(tr);
  });
}

form.onsubmit = async (e) => {
  e.preventDefault();
  const data = {
    Nombre: form.nombre.value,
    Apellido: form.apellido.value,
    IdServicio: parseInt(form.idServicio.value)
  };
  const id = form.idEmpleado.value;
  const method = id ? "PUT" : "POST";
  const url = id ? `${apiURL}/actualizar` : `${apiURL}/crear`;

  if (id) data.IdEmpleado = parseInt(id);

  await fetch(url, {
    method,
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(data)
  });

  form.reset();
  cargarEmpleados();
};

window.editar = async (id) => {
  const res = await fetch(`${apiURL}/consultar?id=${id}`);
  const e = await res.json();
  form.idEmpleado.value = e.IdEmpleado || e.idEmpleado;
  form.nombre.value = e.Nombre || e.nombre;
  form.apellido.value = e.Apellido || e.apellido;
  form.idServicio.value = e.IdServicio || e.idServicio;
};

window.eliminar = async (id) => {
  if (confirm("¿Eliminar empleado?")) {
    await fetch(`${apiURL}/eliminar?id=${id}`, { method: "DELETE" });
    cargarEmpleados();
  }
};

cargarEmpleados();
