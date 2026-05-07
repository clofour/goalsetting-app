import { Button, Group, NumberInput, Stack, Textarea, TextInput } from "@mantine/core";
import { useForm, schemaResolver } from "@mantine/form";
import { postApiGoalCreateBearing } from "@/api/endpoints/goal/goal.js";
import { PostApiGoalCreateBearingBody } from "@/api/endpoints/goal/goal.zod.js";
import { getErrorMessage } from "@/data/error";

interface EventFormProps {
    close: () => void;
    setAlert: (alert: string) => void;
}

export default function EventForm({close, setAlert}: EventFormProps) {
    const formSchema = PostApiGoalCreateBearingBody.omit({
        northStarId: true
    })
    const form = useForm({
        mode: 'uncontrolled',
        validate: schemaResolver(formSchema, { sync: true })
    })

    const handleSubmit = async (values: typeof form.values) => {
        const requestData = {
            ...values
        }
        const response = await postApiGoalCreateBearing(requestData);

        if (response.status === 200) {
            close();
        } else {
            setAlert(response.data ?? getErrorMessage(response.status));
        }
    };

    return (
        <>
            <form onSubmit={form.onSubmit(handleSubmit)}>
                <Stack>
                    <NumberInput
                        label="Amount"
                        description="What is your goal?"
                        placeholder="1"
                        required
                        key={form.key('amount')}
                        {...form.getInputProps('amount')}
                    />

                    <NumberInput
                        label="Amount"
                        description="What is your goal?"
                        placeholder="1"
                        required
                        key={form.key('amount')}
                        {...form.getInputProps('amount')}
                    />
                    

                    <Group justify="flex-end" mt="md">
                        <Button type="submit">Submit</Button>
                    </Group>
                </Stack>
            </form>
        </>
    )
}