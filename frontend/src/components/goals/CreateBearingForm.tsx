import { Button, Group, Stack, Textarea, TextInput } from "@mantine/core";
import { useForm, schemaResolver } from "@mantine/form";
import { postApiGoalCreateBearing } from "@/api/endpoints/goal/goal.js";
import { PostApiGoalCreateBearingBody } from "@/api/endpoints/goal/goal.zod.js";
import { getErrorMessage } from "@/data/error";

interface CreateBearingFormProps {
    close: () => void;
    setAlert: (alert: string) => void;
    parentId: string;
}

export default function CreateBearingForm({close, setAlert, parentId}: CreateBearingFormProps) {
    const formSchema = PostApiGoalCreateBearingBody.omit({
        northStarId: true
    })
    const form = useForm({
        mode: 'uncontrolled',
        validate: schemaResolver(formSchema, { sync: true })
    })

    const handleSubmit = async (values: typeof form.values) => {
        const requestData = {
            ...values,
            northStarId: parentId
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
                    <TextInput
                        label="Name"
                        description="What is your goal?"
                        placeholder="Be healthy"
                        required
                        key={form.key('name')}
                        {...form.getInputProps('name')}
                    />
                    <Textarea
                        label="Description"
                        description="What does your goal consist of?"
                        placeholder="Be healthy"
                        required
                        key={form.key('description')}
                        {...form.getInputProps('description')}
                    />
                    <Textarea
                        label="Justification"
                        description="How does this goal help you achieve your ideal self? Use research."
                        placeholder="Be healthy"
                        key={form.key('justification')}
                        {...form.getInputProps('justification')}
                    />
                    <Textarea
                        label="Difficulty"
                        description="How difficult will this goal be? Are you ready to take it on?"
                        placeholder="Be healthy"
                        key={form.key('difficulty')}
                        {...form.getInputProps('difficulty')}
                    />

                    <Group justify="flex-end" mt="md">
                        <Button type="submit">Submit</Button>
                    </Group>
                </Stack>
            </form>
        </>
    )
}