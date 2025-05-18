// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const value = document.querySelector("#rangeValue");
const input = document.querySelector("#priceRange");
value.textContent = input.value;
input.addEventListener("input", (event) => {
  value.textContent = event.target.value;
});

