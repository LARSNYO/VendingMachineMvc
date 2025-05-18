function setupPaymentScript(totalPrice, denominations) {
    function updateSums() {
        let total = 0;
        denominations.forEach(denom => {
            const qty = parseInt(document.getElementById(`qty-${denom}`).value) || 0;
            const sum = qty * denom;
            document.getElementById(`sum-${denom}`).innerText = sum + ' ₽';
            total += sum;
        });

        document.getElementById('totalSum').innerText = total + ' ₽';

        const payBtn = document.getElementById('payBtn');
        if (total >= totalPrice) {
            payBtn.classList.remove('btn-danger');
            payBtn.classList.add('btn-success');
            payBtn.disabled = false;
            payBtn.innerText = "Оплатить";
        } else {
            payBtn.classList.remove('btn-success');
            payBtn.classList.add('btn-danger');
            payBtn.disabled = true;
            payBtn.innerText = "Недостаточно средств";
        }
    }

    function adjustQty(denom, delta) {
        const input = document.getElementById(`qty-${denom}`);
        let val = parseInt(input.value) || 0;
        val = Math.max(0, val + delta);
        input.value = val;
        updateSums();
    }

    document.querySelectorAll('.qty-input').forEach(input => {
        input.addEventListener('input', updateSums);
    });

    window.addEventListener('load', updateSums);

    // Сделаем adjustQty глобальной (если вызывается из onclick)
    window.adjustQty = adjustQty;
}
