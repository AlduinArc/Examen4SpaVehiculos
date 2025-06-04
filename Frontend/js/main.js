export async function fetchAPI(endpoint, method = 'GET', data = null) {
  const options = {
    method,
    headers: { 'Content-Type': 'application/json' },
  };
  if (data) options.body = JSON.stringify(data);

    const res = await fetch(`http://spavehiculosproy.runasp.net/api/${endpoint}`, options);
  if (!res.ok) throw new Error(`Error en ${method} ${endpoint}`);
  return await res.json();
}

export function crearElementoAuto(auto, editar, eliminar) {
  const div = document.createElement("div");
  div.className = "p-3 border rounded flex justify-between items-center";

  div.innerHTML = `
    <div>
      <p class="font-semibold">${auto.marca} ${auto.modelo}</p>
      <p class="text-sm text-gray-500">${auto.placa}</p>
    </div>
    <div class="space-x-2">
      <button class="text-blue-600">Editar</button>
      <button class="text-red-600">Eliminar</button>
    </div>
  `;

  const [btnEditar, btnEliminar] = div.querySelectorAll("button");
  btnEditar.addEventListener("click", () => editar(auto));
  btnEliminar.addEventListener("click", () => eliminar(auto.idAuto));

  return div;
}
